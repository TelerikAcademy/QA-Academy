import com.jayway.restassured.RestAssured;
import org.junit.Before;
import org.junit.Test;

import static com.jayway.restassured.RestAssured.expect;
import static com.jayway.restassured.RestAssured.get;
import static org.hamcrest.Matchers.equalTo;

public class SmokeTest {
    @Before
    public final void setup() {
        RestAssured.baseURI = "http://api.openweathermap.org/data/2.5";
    }

    @Test
    public void getExample() {
        get("/weather?q=London,uk&appid=b71f2113276144c39d82fec0f49d7531").then().assertThat().body("name", equalTo("London"));
        get("/weather?q=Bangalore,in&appid=b71f2113276144c39d82fec0f49d7531").then().assertThat().body("name", equalTo("Bangalore"));
    }

    @Test
    public void getStatusCheck() {
        expect().statusCode(200).when().get("/weather?q=London,uk&appid=b71f2113276144c39d82fec0f49d7531");
        expect().statusCode(401).when().get("/weather?q=Bangalore,in&appid=b71f2113276144c391d82fec0f49d7531");
    }

}
