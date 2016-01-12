import unittest
bdLibPath=os.path.abspath(sys.argv[0]+"..")
if not bdLibPath in sys.path: sys.path.append(bdLibPath)
from _lib import *

    
class SmokeTests(unittest.TestCase):
    
    def setUp(self):
        pass
    
    def tearDown(self):
        pass    
    
    def test_001_NavigateToTap(self):
        RunBrowserToUrl("chrome","http://platform.telerik.com")
        wait(TAP.button_CreateApp, 30)
    
    def test_002_CreateApp(self):
        click(TAP.button_CreateApp)
        wait(TAP.button_DoCreateApp, 10)
        type(TAP.label_AppName, "TestProject")
        click(TAP.button_DoCreateApp)
        wait(TAP.label_Code, 30).highlight(1)

if __name__ == '__main__':
    suite = unittest.TestLoader().loadTestsFromTestCase(SmokeTests)

    outfile = open("report.html", "w")
    runner = HTMLTestRunner.HTMLTestRunner(stream=outfile, title='SmokeTests Report' )
    runner.run(suite)
    outfile.close()

