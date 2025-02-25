@echo off

set "PROJECT_NAME=%1"

echo Project Name: %PROJECT_NAME%

echo ===================

echo Run ZFC Tools

echo ===================

zfc.exe -i "../%PROJECT_NAME%/Assembly-CSharp.csproj" -o "../%PROJECT_NAME%/Assets/ConfigTools/Scripts/Generated/ZeroFormatter/ZeroFormatterGenerated.cs" -e

pause

#通用场景 术语库
-i，——input=VALUE [required] analyze的输入路径

-o，——output=VALUE [required]输出路径(文件)或目录基(分隔模式)

-s，——separate [optional, default=false]分隔输出文件

-u，——unuseunityattr [optional, default=false]在ZeroFormatterInitializer上禁用UnityEngine的RuntimeInitializeOnLoadMethodAttribute

-t，——customtypes=VALUE [optional, default=empty]逗号分隔允许自定义类型

-c，——conditionalsymbol=VALUE [optional, default=empty]条件编译器符号

-r，——resolvername=VALUE [optional, default=DefaultResolver]注册CustomSerializer目标

-d，——disallowinternaltype [optional, default=false]不生成内部类型

-e，——propertyenumonly [optional, default=false]只生成属性enum类型

-m，——disallowinmetadata [optional, default=false]不生成元数据类型

-g，——gencomparekeyonly [optional, default=false]除了字典键，不要在EnumEqualityComparer中生成

-n，——namespace=VALUE [optional, default=ZeroFormatter]设置命名空间根名称

-f，——forcedefaultresolver [optional, default=false]强制使用DefaultResolver