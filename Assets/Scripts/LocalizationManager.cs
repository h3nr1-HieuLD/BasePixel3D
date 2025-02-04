using UnityEngine;

public class LocalizationManager
{
	private static LocalizationManager s_instance;

	private LockitInfo m_lockitInfo;

	private NewLockitInfo m_newLockitInfo;

	private string m_fileName = string.Empty;

	public static LocalizationManager Instance
	{
		get
		{
			if (LocalizationManager.s_instance == null)
			{
				LocalizationManager.s_instance = new LocalizationManager();
			}
			return LocalizationManager.s_instance;
		}
	}

	public SystemLanguage CurrentLanguage { get; private set; }

	public SystemLanguage DefaultLanguage
	{
		get
		{
			return SystemLanguage.English;
		}
	}

	public string CurrentLocale { get; private set; }

	public string DefaultLocale
	{
		get
		{
			return "en-US";
		}
	}

	public void Init(string text)
	{
		this.CurrentLanguage = Application.systemLanguage;
		this.CurrentLocale = SystemToolsWrapper.GetLocale();
		this.m_newLockitInfo = new NewLockitInfo(text);


		//DataManager.Instance.GetNewLockit(delegate (NewLockitInfo lockitInfo)
		//{
		//	this.m_newLockitInfo = lockitInfo;
		//	var s = new SerializableDictionary<string, Dictionary<string, string>>();
		//	foreach(var kv in lockitInfo.m_dict)
		//	{
		//		s[kv.Key] = kv.Value;
		//	}

		//	//File.WriteAllText(@"D:\Cocos\Tools\UnityDecompiler\Projects\Localization.txt", sw.ToString());
		//	//DataContractSerializer serializer = new DataContractSerializer(lockitInfo.m_dict.GetType());

		//	using (StringWriter sw = new StringWriter())
		//	{
		//		using (XmlTextWriter writer = new XmlTextWriter(sw))
		//		{
		//			// add formatting so the XML is easy to read in the log
		//			writer.Formatting = Formatting.Indented;

		//			s.WriteXml(writer);
		//			//serializer.WriteObject(writer, lockitInfo.m_dict);

		//			writer.Flush();

		//			File.WriteAllText(@"D:\Cocos\Tools\UnityDecompiler\Projects\Localization.txt", sw.ToString());
		//		}
		//	}
		//});
	}

	public string GetString(string key)
	{
		try
		{
			key = key.Trim('\r', '\n');
			return this.m_newLockitInfo[key].Trim('\r', '\n');
		}
		catch
		{
			return key;
		}
	}
}


