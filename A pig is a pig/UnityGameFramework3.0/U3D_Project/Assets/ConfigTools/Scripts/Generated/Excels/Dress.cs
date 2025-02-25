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
	public class Dress
	{
		//唯一ID (80000-90000)
		[Index(0)]
		public virtual int key{ get; set; }

		//名称
		[Index(1)]
		public virtual string name{ get; set; }

		//icon
		[Index(2)]
		public virtual string icon{ get; set; }

		//部位
		[Index(3)]
		public virtual int itemType{ get; set; }

		//职业
		[Index(4)]
		public virtual int job{ get; set; }

		//品质
		[Index(5)]
		public virtual int rare{ get; set; }

		//绑定状态
		[Index(6)]
		public virtual bool bound{ get; set; }

		//描述
		[Index(7)]
		public virtual string describe{ get; set; }

	}
}
