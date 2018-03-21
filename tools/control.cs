using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

class control
{
	[STAThread]
	public static void Main(string[] args)
	{
		if(!((args.Length == 2 && Array.Exists(new string[]{"key"}, E => E == args[0])) || (args.Length == 3 && Array.Exists(new string[]{"move","rdown","rup","ldown","lup","rclick","lclick"}, E => E == args[0])))){
			Console.Write("Error");
			Environment.Exit(-1);
		}
		switch (args[0]) {
			case "key":
				key(args[1]);
				break;
			case "move":
				mousemove(args[1],args[2]);
				break;
			case "rdown":
				rightdown(args[1],args[2]);
				break;
			case "ldown":
				leftdown(args[1],args[2]);
				break;
			case "rup":
				rightup(args[1],args[2]);
				break;
			case "lup":
				leftup(args[1],args[2]);
				break;
			case "rclick":
				rightclick(args[1],args[2]);
				break;
			case "lclick":
				leftclick(args[1],args[2]);
				break;
		}
		Console.Write("Ok");
	}

	[DllImport("user32.dll")]
	private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);

    private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
    private const uint MOUSEEVENTF_LEFTUP = 0x04;
    private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const uint MOUSEEVENTF_RIGHTUP = 0x10;

	public static void rightdown(string a, string b){
		mousemove(a,b);
		mouse_event(MOUSEEVENTF_RIGHTDOWN, Convert.ToUInt32(a), Convert.ToUInt32(b), 0, new IntPtr());
	}

	public static void leftdown(string a, string b){
		mousemove(a,b);
		mouse_event(MOUSEEVENTF_LEFTDOWN, Convert.ToUInt32(a), Convert.ToUInt32(b), 0, new IntPtr());
	}

	public static void rightup(string a, string b){
		mousemove(a,b);
		mouse_event(MOUSEEVENTF_RIGHTUP, Convert.ToUInt32(a), Convert.ToUInt32(b), 0, new IntPtr());
	}

	public static void leftup(string a, string b){
		mousemove(a,b);
		mouse_event(MOUSEEVENTF_LEFTUP, Convert.ToUInt32(a), Convert.ToUInt32(b), 0, new IntPtr());
	}

	public static void rightclick(string a, string b){
		rightdown(a,b);
		rightup(a,b);
	}

	public static void leftclick(string a, string b){
		leftdown(a,b);
		leftup(a,b);
	}

	public static void mousemove(string a, string b){
		Cursor.Position = new Point(int.Parse(a), int.Parse(b));
	}

	public static void key(string key){
		SendKeys.SendWait(key);
	}
}
