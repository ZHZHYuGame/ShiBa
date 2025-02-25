/*  工具菜单 [ ConfigTools/生成关联脚本 ]
 *
 *  当前文件由工具类生成, 请不要手动修改 !
 *
 *  上次生成时间 : 2025/1/14 15:29:28
 */

using System.Collections.Generic;
using global::ZeroFormatter;

namespace ConfigTools
{

	public partial class DataMgr 
	{

		public Dictionary<int, Chat> chatData;
		public Dictionary<int, Dress> dressData;
		public Dictionary<int, Equip> equipData;
		public Dictionary<int, Gift> giftData;
		public Dictionary<int, Item> itemData;
		public Dictionary<int, NewShop> newshopData;
		public Dictionary<int, Shop> shopData;
		public Dictionary<int, Test_9_11> test_9_11Data;
		public Dictionary<int, Tutorial> tutorialData;

		public DataMgr()
		{
			loadDataDic.Add("Chat.bytes", (bytes) => { ZeroFormatterRegister.Deserialize(bytes, out chatData);});
			loadDataDic.Add("Dress.bytes", (bytes) => { ZeroFormatterRegister.Deserialize(bytes, out dressData);});
			loadDataDic.Add("Equip.bytes", (bytes) => { ZeroFormatterRegister.Deserialize(bytes, out equipData);});
			loadDataDic.Add("Gift.bytes", (bytes) => { ZeroFormatterRegister.Deserialize(bytes, out giftData);});
			loadDataDic.Add("Item.bytes", (bytes) => { ZeroFormatterRegister.Deserialize(bytes, out itemData);});
			loadDataDic.Add("NewShop.bytes", (bytes) => { ZeroFormatterRegister.Deserialize(bytes, out newshopData);});
			loadDataDic.Add("Shop.bytes", (bytes) => { ZeroFormatterRegister.Deserialize(bytes, out shopData);});
			loadDataDic.Add("Test_9_11.bytes", (bytes) => { ZeroFormatterRegister.Deserialize(bytes, out test_9_11Data);});
			loadDataDic.Add("Tutorial.bytes", (bytes) => { ZeroFormatterRegister.Deserialize(bytes, out tutorialData);});
		}
	}
}
