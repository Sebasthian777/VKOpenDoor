using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace CustomMouseEvent
{
	public class CMouseEvent
	{
		#region Constructores/Destructores
		public CMouseEvent ()
		{
			Correr(true);
		}
		public CMouseEvent (bool Instanciar)
		{
			Correr(Instanciar);
		}
		~CMouseEvent()
		{
			Detener (true,true);
		}
#endregion
		
		#region Variables
		private int HandleMouseHook = 0;
		private static MetodoHook ProcedimientoMouse;
#endregion
		
		#region Metodos
		//que al pedo hacer esto 2 veces xD
		public void Correr (bool Instanciar)
		{
			if (HandleMouseHook == 0 && Instanciar) 
			{
				ProcedimientoMouse = new MetodoHook(procMouse);
				HandleMouseHook = SetWindowsHookEx(WH_MOUSE_LL,ProcedimientoMouse,
				                                   Marshal.GetHINSTANCE (Assembly.GetExecutingAssembly().GetModules ()[0]),0);
				if (HandleMouseHook==0)
				{
					int error = Marshal.GetLastWin32Error();
					Detener(true, false);
					throw new Win32Exception(error);
				}
			}
		}
		public void Detener (bool desinstalar, bool excepcion)
		{
			if (HandleMouseHook != 0 && desinstalar) 
			{
				int muse = UnhookWindowsHookEx(HandleMouseHook);
				HandleMouseHook = 0;
				if (muse == 0 && excepcion)
				{
					int error = Marshal.GetLastWin32Error();
					throw new Win32Exception(error);
				}
			}
			
		}
		
		// la posta de la milanga es que wParam viene con el 
		// handle de que boton se apreto, y lParam el puntero a la
		// estructura...
		private int procMouse (int Code, int wParam, IntPtr lParam)
		{
			if ((Code >= 0) && (OnMouseActivities != null)) 
			{
				//ahora le dicen Marshal, yo le digo castear...
				//es una mentira como los esquimales y la reina de inglaterra
				GreaterMouseStruct ratonStruct = 
					(GreaterMouseStruct)Marshal.PtrToStructure(lParam,typeof(GreaterMouseStruct));
				
				//uso el mouseButtons de System.Windows.Forms
				MouseButtons button = MouseButtons.None;
				//maquinola de estado para el wParam
				short ruedaDelMouse=0;
				switch (wParam)
				{
					// a ver cuantos botones tiene un raton (?)
				case WM_LBUTTONDOWN:
					button = MouseButtons.Left;
					break;
					
				case WM_RBUTTONDOWN:
					button = MouseButtons.Right;
					break;
					
				case WM_MBUTTONDOWN:
					button = MouseButtons.Middle;
					break;
					
				case WM_MOUSEWHEEL:
					ruedaDelMouse = (short)((ratonStruct.datosMouse >> 16) & 0xffff);
					break;
				}
				OnMouseActivities(this,new MouseEventArgs(button,1,ratonStruct.point.x,ratonStruct.point.y,ruedaDelMouse));
			} 
			return CallNextHookEx(HandleMouseHook,Code,wParam,lParam);
		}
#endregion
		
		#region Eventos
		public event MouseEventHandler OnMouseActivities;
#endregion
		#region DllImport de Win32
		private delegate int MetodoHook(int nCode, int wParam, IntPtr lParam);
		[DllImport("user32")]
		private static extern int GetKeyboardState(byte[] ptrKeyStatre);
		
		[DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
		private static extern int GetKeyState(int key);
		
		[DllImport("user32.dll",CharSet = CharSet.Auto,CallingConvention=CallingConvention.StdCall,SetLastError = true)]
		private static extern int SetWindowsHookEx(int idHook, MetodoHook lptr, IntPtr hMod, int dwThreadId);
		
		[DllImport("user32.dll", CharSet=CharSet.Auto,CallingConvention=CallingConvention.StdCall,SetLastError=true)]
		private static extern int UnhookWindowsHookEx(int idHook);
		
		[DllImport("user32.dll",CharSet=CharSet.Auto,CallingConvention=CallingConvention.StdCall)]
		private static extern int CallNextHookEx(int idHook,int nCode,int wParam, IntPtr lParam);
		
		[DllImport("user32")]
		private static extern int ToAscii(int VirtualKey,int ScanCode,byte[] lptrKeyState,byte[] lptrTransKey, int State);
		
#endregion
		
		#region Constantes y "Estructuras"
		private const int WH_MOUSE_LL       = 14;
		private const int WH_MOUSE          = 7;
		private const int WM_MOUSEMOVE      = 0x200;
		private const int WM_LBUTTONDOWN    = 0x201;
		private const int WM_LBUTTONUP      = 0x202;
		// y la 203???
		private const int WM_RBUTTONDOWN    = 0x204;
		// y la 207???
		private const int WM_RBUTTONUP      = 0x205;
		private const int WM_RBUTTONDBLCLK  = 0x206;
		private const int WM_MBUTTONDOWN    = 0x207;
		// Y la 208???
		private const int WM_MBUTTONDBLCLK  = 0x209;
		private const int WM_MOUSEWHEEL     = 0x020A;
		// y Candela? y la moto? 
		// averiguar que mensajes de windows son para el resto de los
		// botones del mouse...
		
		
		
		/*
		 * Por si se va a usar el teclado...
		private const byte VK_SHIFT     = 0x10;
        private const byte VK_CAPITAL   = 0x14;
        private const byte VK_NUMLOCK   = 0x90;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
		*/
		[StructLayout(LayoutKind.Sequential)]
		private class EstructuraMouse
		{
			public PointCustom punto;
			public int hwnd;
			public int test;
			public int infoExtras;
		}
		[StructLayout(LayoutKind.Sequential)]
		private class PointCustom
		{
			public int x;
			public int y;
		}
		[StructLayout(LayoutKind.Sequential)]
		private class GreaterMouseStruct
		{
			public PointCustom point;
			public int datosMouse;
			public int flgs;
			public int tiempo;
			public int infoExtra;
		}
		
		
#endregion
	}
}

