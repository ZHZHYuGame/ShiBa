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
	public class Gift
	{
		//道具ID
		[Index(0)]
		public virtual int id{ get; set; }

		//道具名称
		[Index(1)]
		public virtual string name{ get; set; }

		//搜索类型
		[Index(2)]
		public virtual int search_type{ get; set; }

		//颜色
		[Index(3)]
		public virtual int color{ get; set; }

		//是否绑定
		[Index(4)]
		public virtual int isbind{ get; set; }

		//能否丢弃
		[Index(5)]
		public virtual int candiscard{ get; set; }

		//能否出售
		[Index(6)]
		public virtual int cansell{ get; set; }

		//能否市场出售
		[Index(7)]
		public virtual int market_cansell{ get; set; }

		//出售价格
		[Index(8)]
		public virtual int sellprice{ get; set; }

		//回收类型
		[Index(9)]
		public virtual int recycltype{ get; set; }

		//回收价格
		[Index(10)]
		public virtual int recyclget{ get; set; }

		//堆叠上限
		[Index(11)]
		public virtual int pile_limit{ get; set; }

		//掉落是否统计
		[Index(12)]
		public virtual int isdroprecord{ get; set; }

		//是否广播
		[Index(13)]
		public virtual int isbroadcast{ get; set; }

		//物品有效时长
		[Index(14)]
		public virtual int time_length{ get; set; }

		//物品失效时间
		[Index(15)]
		public virtual int invalid_time{ get; set; }

		//CD时间
		[Index(16)]
		public virtual int colddown{ get; set; }

		//服务端CD
		[Index(17)]
		public virtual int server_colddown{ get; set; }

		//职业限制
		[Index(18)]
		public virtual int limit_prof{ get; set; }

		//性别要求
		[Index(19)]
		public virtual int limit_sex{ get; set; }

		//等级限制
		[Index(20)]
		public virtual int limit_level{ get; set; }

		//绑定银两
		[Index(21)]
		public virtual int coin_bind{ get; set; }

		//绑定元宝
		[Index(22)]
		public virtual int gold_bind{ get; set; }

		//元宝
		[Index(23)]
		public virtual int gold{ get; set; }

		//开启需要元宝
		[Index(24)]
		public virtual int need_gold{ get; set; }

		//是否区分职业
		[Index(25)]
		public virtual int is_check_prof{ get; set; }

		//必出物品
		[Index(26)]
		public virtual int certain_item{ get; set; }

		//礼品数量
		[Index(27)]
		public virtual int item_num{ get; set; }

		//随出的数量
		[Index(28)]
		public virtual int rand_num{ get; set; }

		//物品ID
		[Index(29)]
		public virtual int item_1_id{ get; set; }

		//物品数量
		[Index(30)]
		public virtual int item_1_num{ get; set; }

		//是否绑定
		[Index(31)]
		public virtual int is_bind_1{ get; set; }

		//物品率
		[Index(32)]
		public virtual int item_1_rate{ get; set; }

		//物品ID
		[Index(33)]
		public virtual int item_2_id{ get; set; }

		//物品数量
		[Index(34)]
		public virtual int item_2_num{ get; set; }

		//是否绑定
		[Index(35)]
		public virtual int is_bind_2{ get; set; }

		//物品率
		[Index(36)]
		public virtual int item_2_rate{ get; set; }

		//物品ID
		[Index(37)]
		public virtual int item_3_id{ get; set; }

		//物品数量
		[Index(38)]
		public virtual int item_3_num{ get; set; }

		//是否绑定
		[Index(39)]
		public virtual int is_bind_3{ get; set; }

		//物品率
		[Index(40)]
		public virtual int item_3_rate{ get; set; }

		//物品ID
		[Index(41)]
		public virtual int item_4_id{ get; set; }

		//物品数量
		[Index(42)]
		public virtual int item_4_num{ get; set; }

		//是否绑定
		[Index(43)]
		public virtual int is_bind_4{ get; set; }

		//物品率
		[Index(44)]
		public virtual int item_4_rate{ get; set; }

		//物品ID
		[Index(45)]
		public virtual int item_5_id{ get; set; }

		//物品数量
		[Index(46)]
		public virtual int item_5_num{ get; set; }

		//是否绑定
		[Index(47)]
		public virtual int is_bind_5{ get; set; }

		//物品率
		[Index(48)]
		public virtual int item_5_rate{ get; set; }

		//物品ID
		[Index(49)]
		public virtual int item_6_id{ get; set; }

		//物品数量
		[Index(50)]
		public virtual int item_6_num{ get; set; }

		//是否绑定
		[Index(51)]
		public virtual int is_bind_6{ get; set; }

		//物品率
		[Index(52)]
		public virtual int item_6_rate{ get; set; }

		//物品ID
		[Index(53)]
		public virtual int item_7_id{ get; set; }

		//物品数量
		[Index(54)]
		public virtual int item_7_num{ get; set; }

		//是否绑定
		[Index(55)]
		public virtual int is_bind_7{ get; set; }

		//物品率
		[Index(56)]
		public virtual int item_7_rate{ get; set; }

		//物品ID
		[Index(57)]
		public virtual int item_8_id{ get; set; }

		//物品数量
		[Index(58)]
		public virtual int item_8_num{ get; set; }

		//是否绑定
		[Index(59)]
		public virtual int is_bind_8{ get; set; }

		//物品率
		[Index(60)]
		public virtual int item_8_rate{ get; set; }

		//物品ID
		[Index(61)]
		public virtual int item_9_id{ get; set; }

		//物品数量
		[Index(62)]
		public virtual int item_9_num{ get; set; }

		//是否绑定
		[Index(63)]
		public virtual int is_bind_9{ get; set; }

		//物品率
		[Index(64)]
		public virtual int item_9_rate{ get; set; }

		//物品ID
		[Index(65)]
		public virtual int item_10_id{ get; set; }

		//物品数量
		[Index(66)]
		public virtual int item_10_num{ get; set; }

		//是否绑定
		[Index(67)]
		public virtual int is_bind_10{ get; set; }

		//物品率
		[Index(68)]
		public virtual int item_10_rate{ get; set; }

		//物品ID
		[Index(69)]
		public virtual int item_11_id{ get; set; }

		//物品数量
		[Index(70)]
		public virtual int item_11_num{ get; set; }

		//是否绑定
		[Index(71)]
		public virtual int is_bind_11{ get; set; }

		//物品率
		[Index(72)]
		public virtual int item_11_rate{ get; set; }

		//物品ID
		[Index(73)]
		public virtual int item_12_id{ get; set; }

		//物品数量
		[Index(74)]
		public virtual int item_12_num{ get; set; }

		//是否绑定
		[Index(75)]
		public virtual int is_bind_12{ get; set; }

		//物品率
		[Index(76)]
		public virtual int item_12_rate{ get; set; }

		//物品ID
		[Index(77)]
		public virtual int item_13_id{ get; set; }

		//物品数量
		[Index(78)]
		public virtual int item_13_num{ get; set; }

		//是否绑定
		[Index(79)]
		public virtual int is_bind_13{ get; set; }

		//物品率
		[Index(80)]
		public virtual int item_13_rate{ get; set; }

		//物品ID
		[Index(81)]
		public virtual int item_14_id{ get; set; }

		//物品数量
		[Index(82)]
		public virtual int item_14_num{ get; set; }

		//是否绑定
		[Index(83)]
		public virtual int is_bind_14{ get; set; }

		//物品率
		[Index(84)]
		public virtual int item_14_rate{ get; set; }

		//物品ID
		[Index(85)]
		public virtual int item_15_id{ get; set; }

		//物品数量
		[Index(86)]
		public virtual int item_15_num{ get; set; }

		//是否绑定
		[Index(87)]
		public virtual int is_bind_15{ get; set; }

		//物品率
		[Index(88)]
		public virtual int item_15_rate{ get; set; }

		//物品ID
		[Index(89)]
		public virtual int item_16_id{ get; set; }

		//物品数量
		[Index(90)]
		public virtual int item_16_num{ get; set; }

		//是否绑定
		[Index(91)]
		public virtual int is_bind_16{ get; set; }

		//物品率
		[Index(92)]
		public virtual int item_16_rate{ get; set; }

		//物品ID
		[Index(93)]
		public virtual int item_17_id{ get; set; }

		//物品数量
		[Index(94)]
		public virtual int item_17_num{ get; set; }

		//是否绑定
		[Index(95)]
		public virtual int is_bind_17{ get; set; }

		//物品率
		[Index(96)]
		public virtual int item_17_rate{ get; set; }

		//物品ID
		[Index(97)]
		public virtual int item_18_id{ get; set; }

		//物品数量
		[Index(98)]
		public virtual int item_18_num{ get; set; }

		//是否绑定
		[Index(99)]
		public virtual int is_bind_18{ get; set; }

		//物品率
		[Index(100)]
		public virtual int item_18_rate{ get; set; }

		//需要播报的物品id
		[Index(101)]
		public virtual int boardcast_item_1{ get; set; }

		//需要播报的物品id2
		[Index(102)]
		public virtual int boardcast_item_2{ get; set; }

		//需要播报的物品id3
		[Index(103)]
		public virtual int boardcast_item_3{ get; set; }

		//需要播报的物品id3
		[Index(104)]
		public virtual int boardcast_item_4{ get; set; }

		//需要播报的物品id5
		[Index(105)]
		public virtual int boardcast_item_5{ get; set; }

		//需要播报的物品id6
		[Index(106)]
		public virtual int boardcast_item_6{ get; set; }

		//需要播报的物品id7
		[Index(107)]
		public virtual int boardcast_item_7{ get; set; }

		//需要播报的物品id8
		[Index(108)]
		public virtual int boardcast_item_8{ get; set; }

		//播报对应的传闻id
		[Index(109)]
		public virtual int boardcast_string_id{ get; set; }

		//变性对应物品ID
		[Index(110)]
		public virtual int other_sex_itemid{ get; set; }

		//使用途径
		[Index(111)]
		public virtual int use_msg{ get; set; }

		//战力
		[Index(112)]
		public virtual int power{ get; set; }

		//获取途径
		[Index(113)]
		public virtual string get_msg{ get; set; }

		//装扮类型
		[Index(114)]
		public virtual int appe_type{ get; set; }

		//格子图片
		[Index(115)]
		public virtual int icon_id{ get; set; }

		//使用效果
		[Index(116)]
		public virtual int show_id{ get; set; }

		//是否可直接使用
		[Index(117)]
		public virtual int click_use{ get; set; }

		//弹出UI
		[Index(118)]
		public virtual int open_panel{ get; set; }

		//掉落图标
		[Index(119)]
		public virtual int drop_icon{ get; set; }

		//在背包中的类型
		[Index(120)]
		public virtual int bag_type{ get; set; }

		//是否快速使用
		[Index(121)]
		public virtual int is_tip_use{ get; set; }

		//是否温馨提示
		[Index(122)]
		public virtual int is_remind_open{ get; set; }

		//物品形象展示
		[Index(123)]
		public virtual int is_display_role{ get; set; }

		//特效展示
		[Index(124)]
		public virtual int special_show{ get; set; }

		//整理快速使用
		[Index(125)]
		public virtual int choose_use{ get; set; }

		//礼包类型
		[Index(126)]
		public virtual int gift_type{ get; set; }

		//珍稀浮窗(1显示)
		[Index(127)]
		public virtual int rarefloating{ get; set; }

		//动态弹出
		[Index(128)]
		public virtual int dynamic_show{ get; set; }

	}
}
