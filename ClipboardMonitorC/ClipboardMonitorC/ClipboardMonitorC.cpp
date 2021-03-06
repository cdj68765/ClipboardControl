// ClipboardMonitorC.cpp: 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <windows.h>
#include <iostream>
using namespace std;
#pragma comment(linker,"/subsystem:windows /entry:mainCRTStartup")

int main(int argc, _TCHAR* argv[])
{
	HANDLE THandle = GlobalAlloc(GMEM_FIXED, 1000);//分配内存
	char* Temp = (char*)THandle;//锁定内存，返回申请内存的首地址
	while (true)
	{
		HWND hWnd = NULL;
		OpenClipboard(hWnd);//打开剪切板
		if (IsClipboardFormatAvailable(CF_TEXT))
		{
			HANDLE h = GetClipboardData(CF_TEXT);//获取剪切板数据
			char* p = (char*)GlobalLock(h);
			GlobalUnlock(h);
			if (strcmp(Temp, p))
			{
				EmptyClipboard();//清空剪切板
				HANDLE hHandle = GlobalAlloc(GMEM_FIXED, 1000);//分配内存
				char* pData = (char*)GlobalLock(hHandle);//锁定内存，返回申请内存的首地址
				strcpy(pData, p);
				strcpy(Temp, p);
				SetClipboardData(CF_TEXT, hHandle);//设置剪切板数据
				GlobalUnlock(hHandle);//解除锁定
			}
		}
		CloseClipboard();//关闭剪切板
		Sleep(500);
	}
	return 0;
}