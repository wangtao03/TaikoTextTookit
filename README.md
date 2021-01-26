### 太鼓之达人 文本处理工具

此处存放为本人汉化  3DS3 《太鼓之达人 咚咚！神秘冒险记》所编写的相关工具

------

#### 类库

- ##### TaikoData
  

二进制数据读写及控制字符的处理

- ##### ConsoleProgressBar
  
  简易的控制台进度显示

------

#### NS文本工具 

- ##### SystemDataExportNS
  
  用于导出系统和故事模式文本
  
  > 用法1：直接拖拽 dat 文件到 工具 上  
  > 用法2：SystemDataExportNS story 故事模式的dat目录

------

#### 3DS文本工具(仅测试 3DS3)

- ##### CharaNameConverter3DS
  
  用于导出和生成 CharaName.dat
  
  > 用法：拖放 dat 或 xlsx 到 工具 上
  
- ##### SystemTextConverter3DS
  
  用于导出和生成系统文本
  
> 用法1：拖放 dat 或 xlsx 到 工具 上  
  > 用法2(仅限导出)：拖放 系统文本所在文件夹 到 工具 上	

- ##### StoryTextConverter3DS
  
  用于导出和生成故事模式文本
  
  > 用法1：拖放 dat 或 xlsx 到 工具 上  
  > 用法2(仅限导出)：直接拖放 系统文本所在文件夹 到 工具 上

------

#### 其他辅助工具

- ##### BinProcess.Win（仅Windows平台）

  用于处理解密后的 Lua 文件

  > 用法1：拖放解密后的 lua 文件 到 exe 上  
  > 用法2：拖放 lua 所在文件夹到 exe  上

- ##### Cleaner

  将已生成 dat 文件的 xlsx 文件移动至 _Completed 目录

  > 用法：拖放 dat 所在文件夹 到 工具 上

- ##### Cleaner.Win

  同上 仅Windows平台

- ##### ControllerTesterExcel

  检查 xlsx 文件中的控制字符

  > 用法1：拖放 xlsx 到 工具 上  
  > 用法2：拖放 xlsx 所在文件夹 到 工具 上	

* ##### Msg3DSFinder

  查找 xlsx 文件中 符合正则 “MsgID_3ds\d_\S+_\d{4}” 的文本并存储到 xlsx
  
  > 用法1：拖放 xlsx 所在文件夹 到 工具 上
  
* ##### Transcriber

  用于将NS的文本移植到3DS

  > 用法：有图形界面，自行领会。
