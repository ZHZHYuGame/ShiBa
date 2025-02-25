/*  工具菜单 [ ConfigTools/生成关联脚本 ]
 *
 *  当前文件由工具类生成, 请不要手动修改 !
 *
 *  上次生成时间 : 2025/1/14 15:29:28
 */
using System.Collections.Generic;
using ZeroFormatter;

namespace ConfigTools
{
	[ZeroFormattable]
	public class Test_9_11
	{
		//唯一ID
		[Index(0)]
		public virtual int id{ get; set; }

		//这是一个float参数
		[Index(1)]
		public virtual float floatFlag{ get; set; }

		//测试 bool
		[Index(2)]
		public virtual bool boolFlag{ get; set; }

		//测试 string
		[Index(3)]
		public virtual string strFlag{ get; set; }

		//测试int list
		[Index(4)]
		public virtual int[] intList{ get; set; }

		//测试 float list
		[Index(5)]
		public virtual float[] floatList{ get; set; }

		//测试 bool list
		[Index(6)]
		public virtual bool[] boolList{ get; set; }

		//测试 string list
		[Index(7)]
		public virtual string[] strList{ get; set; }

	}
}
