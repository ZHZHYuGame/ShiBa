using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedpointSystem
{
    public class RedPointNode
    {
        public int redNum;
        public string strKey;
        public Dictionary<string, RedPointNode> children;
        //回调  当节点的红点数发生变化 就通知正在这个回调
        public delegate void RedPointChangeDelegate(int redNum);
        public RedPointChangeDelegate OnRedPointChange;

        //构造函数 对整体进行初始化
        public RedPointNode(string key)
        {
            strKey = key;
            children = new Dictionary<string, RedPointNode>();

        }
    }

    public class RedPointSystem
    {
        //单例
        private static RedPointSystem instance = new RedPointSystem();
        public static RedPointSystem Instance
        {
            get { return instance; }
        }
        //构造函数 初始化根节点
        public RedPointNode root;
        private RedPointSystem()
        {
            this.root = new RedPointNode(RedPointKey.Root);
        }
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RedPointNode AddNode(string key)
        {
            if (FindNode(key) != null)
            {
                return null;
            }
            string[] keys = key.Split('|');//把节点字符串拆分成单独的节点名
            RedPointNode curNode = root;
            curNode.redNum += 1;
            curNode.OnRedPointChange?.Invoke(curNode.redNum);
            //中间的所有分支节点都会给红点数量+1  红点数量发生变化就调用回调函数 来通知ui的显示 刷新
            foreach (string k in keys)
            {
                if (!curNode.children.ContainsKey(k))
                {
                    curNode.children.Add(k, new RedPointNode(k));
                }
                curNode = curNode.children[k];
                curNode.redNum += 1;
                curNode.OnRedPointChange?.Invoke(curNode.redNum);
            }
            return curNode;
        }

        /// <summary>
        /// 寻找节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RedPointNode FindNode(string key)
        {
            //如果找不到节点 就返回错误结果
            string[] keys = key.Split('|');
            RedPointNode curNode = root;
            foreach (string k in keys)
            {
                if (!curNode.children.ContainsKey(k))
                {
                    return null;
                }
                curNode = curNode.children[k];
            }
            return curNode;
        }

        public void DeleteNode(string key)
        {
            if (FindNode(key) == null)
            {
                return;
            }
            DeleteNode(key, root);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <returns></returns>

        public RedPointNode DeleteNode(string key, RedPointNode node)
        {
            //删除 使用了递归操作 从下向上删除 如果底层节点删除顶层也会跟着删除
            string[] keys = key.Split('|');
            if (key == "" || keys.Length == 0)
            {
                node.redNum = Mathf.Clamp(node.redNum - 1, 0, node.redNum);
                node.OnRedPointChange?.Invoke(node.redNum);
                return node;
            }
            string newKey = string.Join("|", keys, 1, keys.Length - 1);
            RedPointNode curNode = DeleteNode(newKey, node.children[keys[0]]);

            node.redNum = Mathf.Clamp(node.redNum - 1, 0, node.redNum);
            node.OnRedPointChange?.Invoke(node.redNum);

            if (curNode.children.Count > 0)
            {
                foreach (RedPointNode child in curNode.children.Values)
                {
                    if (child.redNum == 0)
                    {
                        child.children.Remove(child.strKey);
                    }
                }
            }
            return node;
        }
        /// <summary>
        /// 添加回调函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cb"></param>
        public void SetCallBack(string key, RedPointNode.RedPointChangeDelegate cb)
        {
            //找到节点 进行回调函数
            RedPointNode node = FindNode(key);
            if (node == null)
            {
                return;
            }
            node.OnRedPointChange += cb;
        }
        /// <summary>
        /// 获取红点数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetRedpointNum(string key)
        {
            //返回节点的红点数量
            RedPointNode node = FindNode(key);
            if (node == null)
            {
                return 0;
            }
            return node.redNum;
        }

    }
    /// <summary>
    /// 红点中的key 可以理解为模块名称  
    /// 因为需要 ”|“进行分割 红点系统利用的是字符串作为红点的键,这样在切割时会造成一定的GC
    /// </summary>
    public class RedPointKey
    {
        public const string Root = "Root";

        public const string Play = "Top_up";
        public const string Play_LEVEL1_TOP = "Play|LevelTop";
        //public const string Play_LEVEL1 = "Play|Level1";
        //public const string Play_LEVEL1_HOME = "Play|Level1|HOME";
        //public const string Play_LEVEL1_SHOP = "Play|Level1|SHOP";
        //public const string Play_LEVEL2 = "Play|Level2";
        //public const string Play_LEVEL2_HOME = "Play|Level2|HOME";
        //public const string Play_LEVEL2_SHOP = "Play|Level2|SHOP";
    }
}
