// 下列 ifdef 块是创建使从 DLL 导出更简单的
// 宏的标准方法。此 DLL 中的所有文件都是用命令行上定义的 LIBSTHOOK_EXPORTS
// 符号编译的。在使用此 DLL 的
// 任何项目上不应定义此符号。这样，源文件中包含此文件的任何其他项目都会将
// LIBSTHOOK_API 函数视为是从 DLL 导入的，而此 DLL 则将用此宏定义的
// 符号视为是被导出的。
#ifdef LIBSTHOOK_EXPORTS
#define LIBSTHOOK_API __declspec(dllexport)
#else
#define LIBSTHOOK_API __declspec(dllimport)
#endif


extern BOOL g_enableTerminate;
extern BOOL g_unhookKeyboard;
extern BOOL g_exitStMain;
extern BOOL g_noTopMostWindow;
extern BOOL g_useFakeImage;
extern WCHAR g_fakeImagePath[MAX_PATH];

extern "C" {
	LIBSTHOOK_API BOOL SetGlobalHook();
	LIBSTHOOK_API BOOL UnsetGlobalHook();
	LIBSTHOOK_API VOID SetEnableTerminate(BOOL x);
	LIBSTHOOK_API VOID SetUnhookKeyboard(BOOL x);
	LIBSTHOOK_API VOID SetExitStMain(BOOL x);
	LIBSTHOOK_API VOID SetNoTopMostWindow(BOOL x);
	LIBSTHOOK_API VOID SetShowConsole(BOOL x);
	LIBSTHOOK_API BOOL IsAlive();
	LIBSTHOOK_API VOID SetFakeImagePath(LPCWSTR x);
}

extern BOOL SetAPIHooks();
extern BOOL UnsetAPIHooks();
extern BOOL SetStModulesProc();
extern VOID SetupDebugConsole();
extern VOID ObtainBasicInformation();
