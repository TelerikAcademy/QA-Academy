import io.appium.java_client.MobileElement;
import io.appium.java_client.android.AndroidDriver;
import io.appium.java_client.remote.MobileCapabilityType;
import io.appium.java_client.service.local.AppiumDriverLocalService;
import io.appium.java_client.service.local.AppiumServiceBuilder;
import io.appium.java_client.service.local.flags.AndroidServerFlag;
import io.appium.java_client.service.local.flags.GeneralServerFlag;
import org.junit.*;
import org.openqa.selenium.By;
import org.openqa.selenium.remote.DesiredCapabilities;

import java.io.File;
import java.util.List;

public class SmokeTest {
    private AndroidDriver<MobileElement> driver;
    private static AppiumDriverLocalService service;

    @BeforeClass
    public static void beforeClass() throws Exception {
        AppiumServiceBuilder serviceBuilder = new AppiumServiceBuilder()
                .usingAnyFreePort()
                .withArgument(AndroidServerFlag.AVD, "Emulator-Api19-Default")
                .withArgument(AndroidServerFlag.AVD_ARGS, "-scale 0.5")
                .withArgument(GeneralServerFlag.LOG_LEVEL, "warn");
        service = AppiumDriverLocalService.buildService(serviceBuilder);
        service.start();
    }

    @Before
    public void setup() throws Exception {
        if (service == null || !service.isRunning())
            throw new RuntimeException("An appium server node is not started!");

        File appDir = new File("../testapp");
        File app = new File(appDir, "android-rottentomatoes-demo-debug.apk");
        DesiredCapabilities capabilities = new DesiredCapabilities();
        capabilities.setCapability(MobileCapabilityType.DEVICE_NAME, "Android Emulator");
        capabilities.setCapability(MobileCapabilityType.APP, app.getAbsolutePath());
        driver = new AndroidDriver<>(service.getUrl(), capabilities);
    }

    @After
    public void tearDown() throws Exception {
        driver.quit();
    }

    @Test
    public void smokeTest() {

        // Tap first 3 list view items and navigate back

        MobileElement listView = driver.findElement(By.className("android.widget.ListView"));
        List<MobileElement> listViewItems = driver.findElements(By.xpath("//android.widget.ListView/android.widget.RelativeLayout"));
        for (int i = 0; i < 3; i++) {
            listViewItems.get(i).tap(1, 500);
            MobileElement score = driver.findElement(By.id("com.codepath.example.rottentomatoes:id/tvAudienceScore"));
            System.out.println("Score: " + score.getText());
            driver.navigate().back();
            listView = driver.findElement(By.className("android.widget.ListView"));
        }
    }

    @AfterClass
    public static void afterClass() {
        if (service != null)
            service.stop();
    }
}
