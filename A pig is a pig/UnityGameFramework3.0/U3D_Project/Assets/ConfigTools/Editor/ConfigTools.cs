/* 1. excel 的读取
 * 2. 对应的c# 代码生成
 * 3. byte文件生成 (导出的配置表数据)
 * 4. 文件修改检测 (在导出配置文件的时候 ,会检测修改过的文件)
 */

using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using ZeroFormatter;

namespace ConfigTools
{
    public static class ConfigTools
    {
        /// <summary>
        /// 配置表信息
        /// </summary>
        private struct ConfigData
        {
            public string className;
            public List<string> argumentDesList;
            public List<string> argumentTypeList;
            public List<string> argumentNameList;
            public List<string[]> argumentDataList;
        }
        
        #region Path

        private static bool _isInitPath;
        
        // excel路径
        private static readonly string _excelsPath = Path.Combine(Application.dataPath,"../..", "Excels");

        // 生成脚本路径
        private static readonly string _generateScriptsPath = Path.Combine(Application.dataPath,"ConfigTools", "Scripts", "Generated");

        // 生成配置表路径
        private static readonly string _generateBytesPath = Path.Combine(Application.dataPath, "GameMain", "Excels");

        // MD5文件路径
        private static readonly string _excelsMD5JsonPath = Path.Combine(_excelsPath, "ExcelsMD5Json.txt");
        
#if UNITY_STANDALONE_WIN

        // tools工具路径
        private static readonly string _zfcBatPath = Path.Combine(Application.dataPath,"../..", "Tools");

#endif
        
        #endregion

        private static readonly List<ConfigData> _configDataList = new List<ConfigData>() ;

        private static Dictionary<string, string> _excelsMd5 = new Dictionary<string, string>();

        #region Menu Item

        [MenuItem("ConfigTools/生成关联脚本")]
        public static void MenuGenerateExcelScripts()
        {
            
            Debug.Log($"Generate Scripts Start. Excels Path:{_excelsPath}");

            ReadExcelArguments();
            
            // // 生成客户端配置
            GenerateAllScripts("ConfigTools", _generateScriptsPath);
            Debug.Log($"Generate Client Scripts End. Class Path:{_generateScriptsPath}");

            AssetDatabase.Refresh();
            
            GenerateZFCScriptsByBat();
        }

        [MenuItem("ConfigTools/导出byte文件")]
        public static void MenuGenerateConfigs()
        {
            
            Debug.Log($"Generate Configs Start. Excels Path:{_excelsPath}");

            ReadExcelData();

            // 生成客户端文件
            GenerateConfig(_generateBytesPath);
            Debug.Log($"Generate Client Configs End. Class Path:{_generateBytesPath}");

            AssetDatabase.Refresh();
        }

        private static void GenerateAllScripts(string nameSpace, string genPath)
        {
            GenerateScripts(nameSpace, genPath);
            GenerateZFCScripts(nameSpace, genPath);
            GenerateDataMgrScripts(nameSpace, genPath);
        }

        #endregion

        #region Excel Tools

        /// <summary>
        /// 读取参数
        /// </summary>
        private static void ReadExcelArguments()
        {
            var directoryInfo = new DirectoryInfo(_excelsPath);
            var allExcels = directoryInfo.GetFiles("*.xlsx");
            var fileName = string.Empty;
            try
            {
                _configDataList.Clear();
                for (int i = 0; i < allExcels.Length; i++)
                {
                    // 排除临时文件
                    var file = allExcels[i];
                    if (file.Name.StartsWith("~$"))
                    {
                        continue;
                    }

                    EditorUtility.DisplayProgressBar("获取配置表字段信息", file.Name, (float)(i - 1) / allExcels.Length);

                    fileName = file.Name;
                    var arguments = new ConfigData
                    {
                        className = file.Name.Replace(".xlsx", ""),
                        argumentDesList = new List<string>(),
                        argumentTypeList = new List<string>(),
                        argumentNameList = new List<string>()
                    };

                    using (var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (var pck = new ExcelPackage(fileStream))
                        {
                            var sheet = pck.Workbook.Worksheets[1];     // 获取第一个表
                            var endAddress = sheet.Dimension.End;        // 获取最后一个表格的位置

                            // 获取描述
                            arguments.argumentDesList.AddRange(sheet.Cells[1, 1, 1, endAddress.Column].Select(c => c.Value?.ToString()));

                            // 获取类型
                            arguments.argumentTypeList.AddRange(sheet.Cells[2, 1, 2, endAddress.Column]
                                .Where(c => c.Value != null && !c.Value.ToString().StartsWith("#"))
                                .Select(c => c.Value?.ToString()));

                            // 获取名字
                            arguments.argumentNameList.AddRange(sheet.Cells[3, 1, 3, endAddress.Column].Select(c => c.Value?.ToString()));
                        }
                    }

                    _configDataList.Add(arguments);
                }
            }
            catch (IOException e)
            {
                Debug.LogError(e.Message);

                // 显示警告窗口
                EditorUtility.DisplayDialog("注意", $"{fileName}配置表无法读取, 请检查路径或文件是否被占用 !!! ", "确认", "取消");
            }

            EditorUtility.ClearProgressBar();
        }

        /// <summary>
        /// 读取Excel表数据
        /// </summary>
        private static void ReadExcelData()
        {
            var directoryInfo = new DirectoryInfo(_excelsPath);
            var allExcels = directoryInfo.GetFiles("*.xlsx");
            var fileName = string.Empty;
            try
            {
                GetExcelMD5Json();
                _configDataList.Clear();

                for (int i = 0; i < allExcels.Length; i++)
                {
                    // 排除临时文件
                    var file = allExcels[i];
                    fileName = file.Name;
                    if (file.Name.StartsWith("~$"))
                    {
                        continue;
                    }

                    EditorUtility.DisplayProgressBar("获取配置表字段信息", file.Name, (float)(i - 1) / allExcels.Length);

                    // 校验文件MD5 (只生成修改过的表)
                    var md5 = GetFileMD5(file.FullName);
                    if (_excelsMd5.TryGetValue(file.Name, out var oldMd5))
                    {
                        if (md5.Equals(oldMd5))
                        {
                            //continue;
                        }
                    }
                    _excelsMd5[file.Name] = md5;

                    var arguments = new ConfigData
                    {
                        className = file.Name.Replace(".xlsx", ""),
                        argumentDesList = new List<string>(),
                        argumentTypeList = new List<string>(),
                        argumentNameList = new List<string>(),
                        argumentDataList = new List<string[]>()
                    };

                    using (var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (var pck = new ExcelPackage(fileStream))
                        {
                            var sheet = pck.Workbook.Worksheets[1];     // 获取第一个表
                            var endAddress = sheet.Dimension.End;        // 获取最后一个表格的位置

                            // 获取描述
                            arguments.argumentDesList.AddRange(sheet.Cells[1, 1, 1, endAddress.Column].Select(c => c.Value?.ToString()));

                            // 获取类型
                            arguments.argumentTypeList.AddRange(sheet.Cells[2, 1, 2, endAddress.Column]
                                .Where(c => c.Value != null && !c.Value.ToString().StartsWith("#"))
                                .Select(c => c.Value?.ToString()));

                            // 获取名字
                            arguments.argumentNameList.AddRange(sheet.Cells[3, 1, 3, endAddress.Column].Select(c => c.Value?.ToString()));

                            // 获取数据
                            for (int j = 4; j <= endAddress.Row; j++) // 从第4 行读到最后一行 (<= Row)
                            {
                                var dataList = sheet.Cells[j, 1, j, endAddress.Column].Select(c => c.Value?.ToString());
                                arguments.argumentDataList.Add(dataList.ToArray());
                            }
                        }
                    }

                    _configDataList.Add(arguments);
                }

                SetExcelMD5Json();
            }
            catch (IOException e)
            {
                Debug.LogError(e.Message);

                // 显示警告窗口
                EditorUtility.DisplayDialog("注意", $"{fileName}配置表无法读取, 请检查路径或文件是否被占用 !!! ", "确认", "取消");
            }

            EditorUtility.ClearProgressBar();
        }

        private static void GetExcelMD5Json()
        {
            if (File.Exists(_excelsMD5JsonPath))
            {
                var jsonText = File.ReadAllText(_excelsMD5JsonPath);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    _excelsMd5 = JsonConvert.DeserializeObject<Dictionary<string, string>>( jsonText );
                }
                File.Delete(_excelsMD5JsonPath);
            }
        }

        private static void SetExcelMD5Json()
        {
            if (File.Exists(_excelsMD5JsonPath))
            {
                File.Delete(_excelsMD5JsonPath);
            }

            var jsonText = JsonConvert.SerializeObject(_excelsMd5);

            File.WriteAllText(_excelsMD5JsonPath, jsonText);
        }

        #endregion

        #region Generate Scripts

        /// <summary>
        /// 配置表类文件
        /// </summary>
        private static void GenerateScripts(string nameSpace, string genPath)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < _configDataList.Count; i++)
            {
                var argument = _configDataList[i];
                builder.Clear();
                builder.AppendLine("/*  工具菜单 [ ConfigTools/生成关联脚本 ]");
                builder.AppendLine(" *");
                builder.AppendLine(" *  当前文件由工具类生成, 请不要手动修改 !");
                builder.AppendLine(" *");
                builder.AppendLine($" *  上次生成时间 : {DateTime.Now}");
                builder.AppendLine(" */");
                builder.AppendLine("using System.Collections.Generic;");
                builder.AppendLine("using ZeroFormatter;");
                builder.AppendLine();
                builder.AppendLine($"namespace {nameSpace}");
                builder.AppendLine("{");
                builder.AppendLine("\t[ZeroFormattable]");
                builder.AppendLine($"\tpublic class {argument.className}");
                builder.AppendLine("\t{");
                for (int j = 0; j < argument.argumentTypeList.Count; j++)
                {
                    var des = argument.argumentDesList[j];
                    if (des.StartsWith("#"))
                    {
                        continue;
                    }
                    var type = argument.argumentTypeList[j];
                    if (type.Contains("_"))
                    {
                        var t = type.Split('_')[0];
                        type = $"{t}[]";
                    }
                    var name = argument.argumentNameList[j];
                    builder.AppendLine($"\t\t//{des}");
                    builder.AppendLine($"\t\t[Index({j})]");
                    builder.AppendLine($"\t\tpublic virtual {type} {name}" + "{ get; set; }");
                    builder.AppendLine();
                }
                builder.AppendLine("\t}");
                builder.AppendLine("}");

                var scriptPath = Path.Combine(genPath, "Excels", $"{argument.className}.cs");
                using (var writer = File.CreateText(scriptPath))
                {
                    EditorUtility.DisplayProgressBar("正在生成配置表类文件", argument.className, (float)(i - 1) / _configDataList.Count);
                    writer.Write(builder.ToString());
                    writer.Flush();
                }
            }
            EditorUtility.ClearProgressBar();
        }

        /// <summary>
        /// 调用外部工具 生成 ZeroFormatter 配置 文件
        /// </summary>
        private static void GenerateZFCScriptsByBat()
        {
            
#if UNITY_STANDALONE_WIN
            // 获取项目文件夹名称
            var projectPath = Directory.GetParent(Application.dataPath);
            var projectName = new DirectoryInfo(projectPath.FullName).Name;

            // 执行 外部 bat 文件
            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.WorkingDirectory = _zfcBatPath;
                process.StartInfo.FileName = "zfc_gen.bat";
                process.StartInfo.Arguments = projectName;
                process.Start();
            }
#else
            Debug.LogError($"请在window 平台下 执行该指令!!!");
#endif
        }

        private static void GenerateZFCScripts(string nameSpace, string genPath)
        {
            // 生成 ZeroFormatter 序列化关联文件
            var builder = new StringBuilder();
            builder.AppendLine("/*  工具菜单 [ ConfigTools/生成关联脚本 ]");
            builder.AppendLine(" *");
            builder.AppendLine(" *  当前文件由工具类生成, 请不要手动修改 !");
            builder.AppendLine(" *");
            builder.AppendLine($" *  上次生成时间 : {DateTime.Now}");
            builder.AppendLine(" */");
            builder.AppendLine();
            builder.AppendLine("namespace ZeroFormatter");
            builder.AppendLine("{");
            builder.AppendLine("\tusing global::System.Collections.Generic;");
            builder.AppendLine("\tusing global::ZeroFormatter.Formatters;");
            builder.AppendLine($"\tusing {nameSpace};");
            builder.AppendLine();
            builder.AppendLine("\tpublic static class ZeroFormatterRegister");
            builder.AppendLine("\t{");
            builder.AppendLine();
            // builder.AppendLine("\t\t[UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]");
            builder.AppendLine("\t\tpublic static void Register()");
            builder.AppendLine("\t\t{");
            builder.AppendLine("\t\t\tZeroFormatterInitializer.Register();");
            
            foreach (var configData in _configDataList)
            {
                var kayType = configData.argumentTypeList[0];
                builder.AppendLine($"\t\t\tFormatter.RegisterDictionary<DefaultResolver,{kayType}, {configData.className}>();");
            }
            builder.AppendLine("\t\t}");
            builder.AppendLine();
            builder.AppendLine("\t\tpublic static byte[] Serialize( object data )");
            builder.AppendLine("\t\t{");
            builder.AppendLine("\t\t\tvar type = data.GetType();");
            foreach (var configData in _configDataList)
            {
                var kayType = configData.argumentTypeList[0];
                builder.AppendLine($"\t\t\tif (type == typeof(Dictionary<{kayType}, {configData.className}>))");
                builder.AppendLine("\t\t\t{");
                builder.AppendLine($"\t\t\t\treturn ZeroFormatterSerializer.Serialize(data as Dictionary<{kayType}, {configData.className}>);");
                builder.AppendLine("\t\t\t}");
            }
            builder.AppendLine("\t\t\treturn null;");
            builder.AppendLine("\t\t}");
            foreach (var configData in _configDataList)
            {
                var kayType = configData.argumentTypeList[0];
                builder.AppendLine();
                builder.AppendLine($"\t\tpublic static void Deserialize(byte[] bytes, out Dictionary<{kayType}, {configData.className}> dataDic)");
                builder.AppendLine("\t\t{");
                builder.AppendLine($"\t\t\tdataDic = ZeroFormatterSerializer.Deserialize<Dictionary<{kayType}, {configData.className}>>(bytes);");
                builder.AppendLine("\t\t}");
            }
            builder.AppendLine("\t}");
            builder.AppendLine("}");

            var scriptPath = Path.Combine(genPath, "ZeroFormatter", "ZeroFormatterRegister.cs");
            using (var writer = File.CreateText(scriptPath))
            {
                writer.Write(builder.ToString());
                writer.Flush();
            }

        }

        /// <summary>
        /// // 生成 DataMgr 加载文件
        /// </summary>
        private static void GenerateDataMgrScripts(string nameSpace, string genPath)
        {
            var builder = new StringBuilder();
            builder.AppendLine("/*  工具菜单 [ ConfigTools/生成关联脚本 ]");
            builder.AppendLine(" *");
            builder.AppendLine(" *  当前文件由工具类生成, 请不要手动修改 !");
            builder.AppendLine(" *");
            builder.AppendLine($" *  上次生成时间 : {DateTime.Now}");
            builder.AppendLine(" */");
            builder.AppendLine();
            builder.AppendLine("using System.Collections.Generic;");
            builder.AppendLine("using global::ZeroFormatter;");
            builder.AppendLine();
            builder.AppendLine($"namespace {nameSpace}");
            builder.AppendLine("{");
            builder.AppendLine();
            builder.AppendLine("\tpublic partial class DataMgr ");
            builder.AppendLine("\t{");
            builder.AppendLine();
            foreach (var configData in _configDataList)
            {
                var kayType = configData.argumentTypeList[0];
                builder.AppendLine($"\t\tpublic Dictionary<{kayType}, {configData.className}> {configData.className.ToLower()}Data;");
            }
            builder.AppendLine();
            builder.AppendLine("\t\tpublic DataMgr()");
            builder.AppendLine("\t\t{");
            foreach (var configData in _configDataList)
            {
                builder.AppendLine($"\t\t\tloadDataDic.Add(\"{configData.className}.bytes\", (bytes) => {{ ZeroFormatterRegister.Deserialize(bytes, out {configData.className.ToLower()}Data);}});");
            }
            builder.AppendLine("\t\t}");
            builder.AppendLine("\t}");
            builder.AppendLine("}");

            var scriptPath = Path.Combine(genPath, "ZeroFormatter", "DataMgr.cs");
            using (var writer = File.CreateText(scriptPath))
            {
                writer.Write(builder.ToString());
                writer.Flush();
            }
        }

        /// <summary>
        /// 生成配置文件数据
        /// </summary>
        private static void GenerateConfig( string genPath )
        {
            ZeroFormatterRegister.Register();
            
            var className = string.Empty;
            var propertyName = string.Empty;
            var currentKey = string.Empty;
            var dataKay = 0;

            try
            {
                for (int i = 0; i < _configDataList.Count; i++)
                {
                    var configData = _configDataList[i];
                    var valueType = GetType($"ConfigTools.{configData.className}");

                    if (valueType == null) continue;

                    className = configData.className;
                    var properties = valueType.GetProperties(); // 获取所有参数        
                    var kayProperty = properties[0]; // 第一个值为 kay 的类型
                    var dictionaryType =
                        typeof(Dictionary<,>).MakeGenericType(new Type[] { kayProperty.PropertyType, valueType });
                    var dictionary = Activator.CreateInstance(dictionaryType); // 根据类型创建字典
                    var addMethod = dictionary.GetType().GetMethod("Add"); // 获取Add方法

                    if (addMethod == null) continue;

                    for (int j = 0; j < configData.argumentDataList.Count; j++)
                    {
                        dataKay = j + 4;
                        var valueClass = Activator.CreateInstance(valueType); // 根据类型 创建类对象
                        var strDataArray = configData.argumentDataList[j]; // 获取数据
                        var strLength = strDataArray.Length;
                        for (int k = 0; k < properties.Length; k++) // 遍历每个参数
                        {
                            if (configData.argumentDesList[k].StartsWith("#"))
                            {
                                continue;
                            }

                            currentKey = strDataArray[0];

                            var property = properties[k];

                            if (k >= strLength) continue;

                            var valueStr = strDataArray[k];

                            if (string.IsNullOrEmpty(valueStr)) continue;

                            // 给每个参数设置值 
                            propertyName = property.Name;
                            if (property.PropertyType == typeof(int[]))
                            {
                                valueStr = valueStr.Trim('[', ']');
                                var strArray = valueStr.Split(',');
                                property.SetValue(valueClass, strArray.Select(int.Parse).ToArray());
                            }
                            else if (property.PropertyType == typeof(string[]))
                            {
                                valueStr = valueStr.Trim('[', ']');
                                property.SetValue(valueClass, valueStr.Split(','));
                            }
                            else if (property.PropertyType == typeof(float[]))
                            {
                                valueStr = valueStr.Trim('[', ']');
                                var strArray = valueStr.Split(',');
                                property.SetValue(valueClass, strArray.Select(float.Parse).ToArray());
                            }
                            else if (property.PropertyType == typeof(bool[]))
                            {
                                valueStr = valueStr.Trim('[', ']');
                                var strArray = valueStr.Split(',');
                                property.SetValue(valueClass, strArray.Select(bool.Parse).ToArray());
                            }
                            else
                            {
                                property.SetValue(valueClass, Convert.ChangeType(valueStr, property.PropertyType));
                            }
                        }

                        addMethod.Invoke(dictionary, new[] { kayProperty.GetValue(valueClass), valueClass });
                    }

                    ///TODO 生成 序列化标记类后 放开
                    // 序列化加入文件 
                    var bytes = ZeroFormatterRegister.Serialize(dictionary);
                    var bytesPath = Path.Combine(genPath, $"{configData.className}.bytes.txt");
                    using (var fileStream = File.OpenWrite(bytesPath))
                    {
                        using (var binaryWriter = new BinaryWriter(fileStream, Encoding.Unicode))
                        {
                            EditorUtility.DisplayProgressBar("正在序列化配置文件数据", configData.className,
                                (float)(i - 1) / _configDataList.Count);
                            binaryWriter.Write(bytes);
                        }
                    }
                }

                EditorUtility.ClearProgressBar();
            }
            catch (DirectoryNotFoundException e)
            {
                var message = e.InnerException == null ? e.Message : e.InnerException.Message;
                Debug.LogError(message);
                EditorUtility.DisplayDialog("导出失败",
                    $"导出路径错误:{Path.Combine(genPath, $"{className}.bytes.txt")}" , "确认", "取消");
            }
            catch (Exception e)
            {
                var message = e.InnerException == null ? e.Message : e.InnerException.Message;
                Debug.LogError(message);
                EditorUtility.DisplayDialog("导出失败",
                    message.Contains("An item with the same key has already been added.")
                        ? $"序列化 {className} 表时,发现重复ID!!! \r\n 行数 :{dataKay} \r\n 重复ID :{currentKey}"
                        : $"序列化 {className} 表发生错误!!! \r\n 行数 :{dataKay}  \r\n 参数 :{propertyName}", "确认", "取消");
            }
        }
        #endregion

        /// <summary>
        /// 获取文件的MD5
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string GetFileMD5( string path )
        {
            var md5Str = string.Empty;
            if (File.Exists(path))
            {
                using( var file = File.OpenRead(path))
                {
                    var md5Hash = MD5.Create();
                    var bytes = md5Hash.ComputeHash(file);
                    md5Str = BitConverter.ToString(bytes).Replace("-","").ToLower();
                }
            }
            else
            {
                Debug.LogError($"[ GameUtils.GetFileMD5 ] Not Fond File , File Path:{path}");
            }
            return md5Str;
        }
        
        /// <summary>
        /// 根据名字获取类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private static Type GetType( string typeName )
        {
            var type = Type.GetType(typeName);
            
            if (type != null) return type;
            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                type = assembly.GetType( typeName );
                if (type != null)
                {
                    return type;
                }
            }

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                type = types.FirstOrDefault(t=> t.Name == typeName);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
