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
	public class Chat
	{
		//唯一ID
		[Index(0)]
		public virtual int id{ get; set; }

		//消息名称
		[Index(1)]
		public virtual string name{ get; set; }

		//消息类型
		[Index(2)]
		public virtual int chatType{ get; set; }

		//消息颜色
		[Index(3)]
		public virtual string colorStr{ get; set; }

		//消息图片
		[Index(4)]
		public virtual int spriteId{ get; set; }

	}
}
