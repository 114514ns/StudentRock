#include "pch.h"
#include "DetourFunctions.h"
#include "LibStHook.h"

fnTDMasterInitHook TDMasterInitHook = NULL;
fnTDMasterInitHook fpTDMasterInitHook = NULL;

fnGetDC fpGetDC = NULL;
fnCreateDCW fpCreateDCW = NULL;
fnSetWindowPos fpSetWindowPos = NULL;
fnGetDIBits fpGetDIBits = NULL;

HDC WINAPI DetourGetDC(HWND hWnd)
{
	printf("[Info] allowed GetDC: hwnd:%d\n", (int)hWnd);
	return fpGetDC(hWnd);
}

HDC WINAPI DetourCreateDCW(LPCWSTR pwszDriver, LPCWSTR pwszDevice, LPWSTR pszPort, const DEVMODEW* pdm)
{
	return fpCreateDCW(pwszDriver, pwszDevice, pszPort, pdm);
}

BOOL WINAPI DetourSetWindowPos(HWND hWnd, HWND hWndInsertAfter, int  X, int  Y, int  cx, int  cy, UINT uFlags)
{
	if (g_noTopMostWindow && hWndInsertAfter == HWND_TOPMOST) {
		if ((GetWindowLong(hWnd, GWL_STYLE) & WS_BORDER) == 0) {
			SetWindowLong(hWnd, GWL_STYLE, WS_OVERLAPPEDWINDOW);
			printf("[Info] reset Window style.\n");
			fpSetWindowPos(hWnd, hWndInsertAfter, X, Y, cx, cy, SWP_FRAMECHANGED | SWP_NOSIZE | SWP_NOMOVE | SWP_NOZORDER);
		}
		printf("[Info] disallowed set zorder.\n");
		return FALSE;
	}
	printf("[Info] allowed SetWindowPos.\n");
	return fpSetWindowPos(hWnd, hWndInsertAfter, X, Y, cx, cy, uFlags);
}

int WINAPI DetourGetDIBits(HDC hdc, HBITMAP hbm, UINT start, UINT cLines, LPVOID lpvBits, LPBITMAPINFO lpbmi, UINT usage)
{
	if (g_fakeScreenshot && lpvBits != NULL && cLines > 32) {
		// cLines > 32: prevent cursor redraw
		HBITMAP hbm = NULL;
		if (OpenClipboard(NULL)) {
			hbm = (HBITMAP)GetClipboardData(CF_BITMAP);
			CloseClipboard();
			int ret = NULL;
			ret = fpGetDIBits(hdc, hbm, start, cLines, lpvBits, lpbmi, usage);
			printf("[Info] read from clipboard: GetDIBits:%d cLines:%d\n", ret, cLines);
			return ret;
		}
		printf("[Info] disallowed GetDIBits.\n");
		return FALSE;
	}
	printf("[Info] allowed GetDIBits.\n");
	return fpGetDIBits(hdc, hbm, start, cLines, lpvBits, lpbmi, usage);
}

int DetourTDMasterInitHook(HWND hWnd, UINT msg, int a3)
{
	if (g_unhookKeyboard) {
		printf("[Info] disallowed InitHook.\n");
		return FALSE;
	}
	printf("[Info] allowed InitHook.\n");
	return fpTDMasterInitHook(hWnd, msg, a3);
}