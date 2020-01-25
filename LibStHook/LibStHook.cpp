// LibStHook.cpp : 定义 DLL 的导出函数。
//

#include "pch.h"
#include "framework.h"
#include "LibStHook.h"
#include "DetourFunctions.h"
#include "MinHook.h"

typedef BOOL(*fnTDMasterDoneHook)();
typedef BOOL(*fnTDProcHookEnableTerminate)(int);

#pragma data_seg("StHookdata")
HHOOK g_hHook = NULL;
BOOL g_enableTerminate = FALSE;
BOOL g_unhookKeyboard = FALSE;
BOOL g_exitStMain = FALSE;
BOOL g_noTopMostWindow = FALSE;
BOOL g_isAlive = FALSE;
BOOL g_showConsole = FALSE;
BOOL g_useFakeImage = FALSE;
WCHAR g_fakeImagePath[MAX_PATH] = { 0 };
BOOL g_noBlackScreen = FALSE;
#pragma data_seg()
#pragma comment(linker, "/SECTION:StHookdata,RWS")

extern HMODULE g_hDllModule;
extern char szPath[MAX_PATH];

HMODULE hTDProcHook = NULL;
HMODULE hTDMaster = NULL;
fnTDProcHookEnableTerminate TDProcHookEnableTerminate = NULL;
fnTDMasterDoneHook TDMasterDoneHook = NULL;

VOID SetupDebugConsole()
{
	AllocConsole();
	FILE* dummy;
	freopen_s(&dummy, "CONIN$", "r", stdin);
	freopen_s(&dummy, "CONOUT$", "w", stdout);
	char buf[MAX_PATH];
	GetConsoleTitle(buf, MAX_PATH);
	HWND hwnd = FindWindow("ConsoleWindowClass", buf);
	HMENU hmenu = GetSystemMenu(hwnd, FALSE);
	if (hwnd) {
		RemoveMenu(hmenu, SC_CLOSE, MF_BYCOMMAND);
		if (g_showConsole) ShowWindow(hwnd, SW_SHOWDEFAULT);
		else ShowWindow(hwnd, SW_HIDE);
		// SendMessage(hwnd, WM_SYSCOMMAND, SC_MINIMIZE, 0);
	}
}

BOOL SetStModulesProc()
{
	hTDProcHook = GetModuleHandle("LibTDProcHook10.dll");
	if (hTDProcHook == NULL) {
		printf("[Error] cannot load LibTDProcHook10.dll: maybe incorrect StudentMain version.\n");
		return FALSE;
	}
	hTDMaster = GetModuleHandle("LibTDMaster.dll");
	if (hTDMaster == NULL) {
		printf("[Error] cannot load LibTDMaster.dll: maybe incorrect StudentMain version.\n");
		return FALSE;
	}
	TDProcHookEnableTerminate = (fnTDProcHookEnableTerminate)GetProcAddress(hTDProcHook, "TDProcHookEnableTerminate");
	if (TDProcHookEnableTerminate == NULL) {
		printf("[Error] cannot load TDProcHookEnableTerminate.\n");
		return FALSE;
	}
	TDMasterDoneHook = (fnTDMasterDoneHook)GetProcAddress(hTDMaster, "DoneHook");
	if (TDMasterDoneHook == NULL) {
		printf("[Error] cannot load TDMasterDoneHook.\n");
		return FALSE;
	}
	TDMasterInitHook = (fnTDMasterInitHook)GetProcAddress(hTDMaster, "InitHook");
	if (TDMasterInitHook == NULL) {
		printf("[Error] cannot load TDMasterInitHook.\n");
		return FALSE;
	}
	return TRUE;
}

VOID ObtainBasicInformation()
{
	char l_StMainPath[MAX_PATH] = { 0 };
	GetModuleFileName(NULL, l_StMainPath, MAX_PATH);
	printf("[Info] Detected StudentMain: ProcessId=%d Path=%s\n", GetCurrentProcessId(), l_StMainPath);
}

LRESULT GetMsgProc(
	int code,
	WPARAM wParam,
	LPARAM lParam)
{
	if (strcmp(szPath, "StudentMain.exe") != 0) goto end;
	if (g_exitStMain) {
		ExitProcess(0);
		goto end;
	}
	if (g_enableTerminate) {
		if (TDProcHookEnableTerminate == NULL) goto end;

		TDProcHookEnableTerminate(TRUE);
	}
	if (g_unhookKeyboard) {
		if (TDMasterDoneHook == NULL) goto end;

		TDMasterDoneHook();
	}
	if (!g_isAlive) g_isAlive = TRUE;
end:
	return CallNextHookEx(g_hHook, code, wParam, lParam);
}


BOOL SetGlobalHook()
{
	if(g_hHook) UnsetGlobalHook();
	g_hHook = SetWindowsHookExA(WH_GETMESSAGE, (HOOKPROC)GetMsgProc, g_hDllModule, NULL); // SetWindowsHookExW won't work :(
	if (NULL == g_hHook)
	{
		return FALSE;
	}
	return TRUE;
}


BOOL UnsetGlobalHook()
{
	UnhookWindowsHookEx(g_hHook);
	g_hHook = NULL;
	return TRUE;
}


BOOL SetAPIHooks()
{
	if (MH_Initialize() != MH_OK) return FALSE;
	MH_STATUS sts;
	/*
	sts = MH_CreateHook(&GetDC, &DetourGetDC, (LPVOID*)& fpGetDC);
	if (sts != MH_OK) {
		printf("[Error] cannot hook GetDC: %s\n", MH_StatusToString(sts));
		return FALSE;
	}
	*/
	sts = MH_CreateHook(&GetDIBits, &DetourGetDIBits, (LPVOID*)& fpGetDIBits);
	if (sts != MH_OK) {
		printf("[Error] cannot hook GetDC: %s\n", MH_StatusToString(sts));
		return FALSE;
	}
	sts = MH_CreateHook(&SetWindowPos, &DetourSetWindowPos, (LPVOID*)& fpSetWindowPos);
	if (sts != MH_OK) {
		printf("[Error] cannot hook SetWindowPos: %s\n", MH_StatusToString(sts));
		return FALSE;
	}
	sts = MH_CreateHook(&CreateWindowExW, &DetourCreateWindowExW, (LPVOID*)&fpCreateWindowExW);
	if (sts != MH_OK) {
		printf("[Error] cannot hook CreateWindowExW: %s\n", MH_StatusToString(sts));
		return FALSE;
	}
	sts = MH_CreateHook(TDMasterInitHook, &DetourTDMasterInitHook, (LPVOID*)& fpTDMasterInitHook);
	if (sts != MH_OK) {
		printf("[Error] cannot hook TDMasterInitHook: %s\n", MH_StatusToString(sts));
		return FALSE;
	}
	// if (MH_CreateHook(&CreateDCW, &DetourCreateDCW, (LPVOID*)&fpCreateDCW) != MH_OK) return FALSE;

	sts = MH_EnableHook(MH_ALL_HOOKS);
	if (sts != MH_OK) {
		printf("[Error] cannot enable all hooks: %s\n", MH_StatusToString(sts));
		return FALSE;
	}
	return TRUE;
}

BOOL UnsetAPIHooks()
{
	if (MH_Uninitialize() != MH_OK) return FALSE;

	return TRUE;
}

VOID SetEnableTerminate(BOOL x)
{
	g_enableTerminate = x;
	return;
}

VOID SetUnhookKeyboard(BOOL x)
{
	g_unhookKeyboard = x;
	return;
}

VOID SetExitStMain(BOOL x)
{
	g_exitStMain = x;
	return;
}

VOID SetNoTopMostWindow(BOOL x)
{
	g_noTopMostWindow = x;
	return;
}

BOOL IsAlive()
{
	if (g_isAlive) {
		g_isAlive = FALSE;
		return 1;
	}
	return 0;
}

VOID SetShowConsole(BOOL x)
{
	g_showConsole = x;
	return;
}

VOID SetFakeImagePath(LPCWSTR x)
{
	wcscpy_s(g_fakeImagePath, x);
	if (wcslen(x)) {
		g_useFakeImage = TRUE;
	}
	else {
		g_useFakeImage = FALSE;
	}
	return;
}

VOID SetNoBlackScreen(BOOL x)
{
	g_noBlackScreen = x;
	return;
}