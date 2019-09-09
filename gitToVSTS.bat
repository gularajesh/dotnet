cd /D "%~dp0"
git pull
git remote prune origin
git checkout develop
git reset --hard
git clean -f -x -d
RMDIR /S /Q "D:\SynTFS\APAC - Mini Calculator\Server Components\Development\Source"\
MKDIR "D:\SynTFS\APAC - Mini Calculator\Server Components\Development\Source"\
cd src

Xcopy /E /S . "D:\SynTFS\APAC - Mini Calculator\Server Components\Development\Source"\
pause