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
	public class Tutorial
	{
		//唯一ID
		[Index(0)]
		public virtual int id{ get; set; }

		//触发条件
		[Index(1)]
		public virtual int triggerType{ get; set; }

		//触发参数
		[Index(2)]
		public virtual int triggerFlag{ get; set; }

		//高亮类型
		[Index(3)]
		public virtual int lightType{ get; set; }

		//高亮位置
		[Index(4)]
		public virtual int[] lightPos{ get; set; }

		//高亮的大小
		[Index(5)]
		public virtual int[] lightSize{ get; set; }

		//高亮的锚点
		[Index(6)]
		public virtual int lightAnchor{ get; set; }

		//手指类型
		[Index(7)]
		public virtual int fingerType{ get; set; }

		//手指位置
		[Index(8)]
		public virtual int[] fingerPos{ get; set; }

		//手指锚点
		[Index(9)]
		public virtual int fingerAnchor{ get; set; }

		//提示类型
		[Index(10)]
		public virtual int textType{ get; set; }

		//提示位置
		[Index(11)]
		public virtual int[] textPos{ get; set; }

		//提示锚点
		[Index(12)]
		public virtual int textAnchor{ get; set; }

		//提示内容
		[Index(13)]
		public virtual string textStr{ get; set; }

		//结束条件
		[Index(14)]
		public virtual int endType{ get; set; }

		//结束的参数
		[Index(15)]
		public virtual int endFlag{ get; set; }

	}
}
