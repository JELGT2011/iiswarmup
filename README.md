iiswarmup
=========

A windows service which keeps IIS app pools warm. Currently very rudimentary; can only warm up one app pool by a configured URL, and then the service will stop.

This tool is not actively maintained, but if you'd like some features I'm happy to add them, just ask.

**NOTE:** This is only useful with IIS 7 and earlier; IIS 7.5 and onwards provide auto-start. For more information, please see [http://msdn.microsoft.com/en-us/library/ee677285(v=azure.10).aspx](http://msdn.microsoft.com/en-us/library/ee677285\(v=azure.10\).aspx)
## Installation ##

1. Download somewhere
2. Edit the config and add your URL (one URL supported for now; for multiple URLs you currently have to follow these steps multiple times)
3. Open an administrative command prompt:

    1. click Start
    2. Type cmd
    3. Press Ctrl-Shift-Enter and accept the UAC prompt) and install the service:

`sc create "iiswarmup" binPath= "D:\Projects\iiswarmup\bin\De
bug\iiswarmup.exe" start= auto DisplayName= "IIS Warmup - My Site"`

Your service will start the next time the server is rebooted. If you can start it straight away Windows should tell you the service started then stopped; this is by design.

## TODO ##

- Support multiple sites/AppPools
- Find a way to monitor AppPools and warm up after every recycle
- Option to read IIS bindings rather than requiring the user to specify a URL
- Run warmup in a worker thread to avoid service startup timeouts.