/*  工具菜单 [ ConfigTools/生成关联脚本 ]
 *
 *  当前文件由工具类生成, 请不要手动修改 !
 *
 *  上次生成时间 : 2025/1/14 15:29:28
 */

namespace ZeroFormatter
{
	using global::System.Collections.Generic;
	using global::ZeroFormatter.Formatters;
	using ConfigTools;

	public static class ZeroFormatterRegister
	{

		public static void Register()
		{
			ZeroFormatterInitializer.Register();
			Formatter.RegisterDictionary<DefaultResolver,int, Chat>();
			Formatter.RegisterDictionary<DefaultResolver,int, Dress>();
			Formatter.RegisterDictionary<DefaultResolver,int, Equip>();
			Formatter.RegisterDictionary<DefaultResolver,int, Gift>();
			Formatter.RegisterDictionary<DefaultResolver,int, Item>();
			Formatter.RegisterDictionary<DefaultResolver,int, NewShop>();
			Formatter.RegisterDictionary<DefaultResolver,int, Shop>();
			Formatter.RegisterDictionary<DefaultResolver,int, Test_9_11>();
			Formatter.RegisterDictionary<DefaultResolver,int, Tutorial>();
		}

		public static byte[] Serialize( object data )
		{
			var type = data.GetType();
			if (type == typeof(Dictionary<int, Chat>))
			{
				return ZeroFormatterSerializer.Serialize(data as Dictionary<int, Chat>);
			}
			if (type == typeof(Dictionary<int, Dress>))
			{
				return ZeroFormatterSerializer.Serialize(data as Dictionary<int, Dress>);
			}
			if (type == typeof(Dictionary<int, Equip>))
			{
				return ZeroFormatterSerializer.Serialize(data as Dictionary<int, Equip>);
			}
			if (type == typeof(Dictionary<int, Gift>))
			{
				return ZeroFormatterSerializer.Serialize(data as Dictionary<int, Gift>);
			}
			if (type == typeof(Dictionary<int, Item>))
			{
				return ZeroFormatterSerializer.Serialize(data as Dictionary<int, Item>);
			}
			if (type == typeof(Dictionary<int, NewShop>))
			{
				return ZeroFormatterSerializer.Serialize(data as Dictionary<int, NewShop>);
			}
			if (type == typeof(Dictionary<int, Shop>))
			{
				return ZeroFormatterSerializer.Serialize(data as Dictionary<int, Shop>);
			}
			if (type == typeof(Dictionary<int, Test_9_11>))
			{
				return ZeroFormatterSerializer.Serialize(data as Dictionary<int, Test_9_11>);
			}
			if (type == typeof(Dictionary<int, Tutorial>))
			{
				return ZeroFormatterSerializer.Serialize(data as Dictionary<int, Tutorial>);
			}
			return null;
		}

		public static void Deserialize(byte[] bytes, out Dictionary<int, Chat> dataDic)
		{
			dataDic = ZeroFormatterSerializer.Deserialize<Dictionary<int, Chat>>(bytes);
		}

		public static void Deserialize(byte[] bytes, out Dictionary<int, Dress> dataDic)
		{
			dataDic = ZeroFormatterSerializer.Deserialize<Dictionary<int, Dress>>(bytes);
		}

		public static void Deserialize(byte[] bytes, out Dictionary<int, Equip> dataDic)
		{
			dataDic = ZeroFormatterSerializer.Deserialize<Dictionary<int, Equip>>(bytes);
		}

		public static void Deserialize(byte[] bytes, out Dictionary<int, Gift> dataDic)
		{
			dataDic = ZeroFormatterSerializer.Deserialize<Dictionary<int, Gift>>(bytes);
		}

		public static void Deserialize(byte[] bytes, out Dictionary<int, Item> dataDic)
		{
			dataDic = ZeroFormatterSerializer.Deserialize<Dictionary<int, Item>>(bytes);
		}

		public static void Deserialize(byte[] bytes, out Dictionary<int, NewShop> dataDic)
		{
			dataDic = ZeroFormatterSerializer.Deserialize<Dictionary<int, NewShop>>(bytes);
		}

		public static void Deserialize(byte[] bytes, out Dictionary<int, Shop> dataDic)
		{
			dataDic = ZeroFormatterSerializer.Deserialize<Dictionary<int, Shop>>(bytes);
		}

		public static void Deserialize(byte[] bytes, out Dictionary<int, Test_9_11> dataDic)
		{
			dataDic = ZeroFormatterSerializer.Deserialize<Dictionary<int, Test_9_11>>(bytes);
		}

		public static void Deserialize(byte[] bytes, out Dictionary<int, Tutorial> dataDic)
		{
			dataDic = ZeroFormatterSerializer.Deserialize<Dictionary<int, Tutorial>>(bytes);
		}
	}
}
