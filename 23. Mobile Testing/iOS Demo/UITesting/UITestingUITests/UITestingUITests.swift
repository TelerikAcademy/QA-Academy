//
//  UITestingUITests.swift
//  UITestingUITests
//
//  Created by Miroslava Ivanova on 12/23/15.
//  Copyright © 2015 Telerik. All rights reserved.
//

import XCTest

class UITestingUITests: XCTestCase {
        
    override func setUp() {
        super.setUp()
        
        // Put setup code here. This method is called before the invocation of each test method in the class.
        
        // In UI tests it is usually best to stop immediately when a failure occurs.
        continueAfterFailure = false
        // UI tests must launch the application that they test. Doing this in setup will make sure it happens for each test method.
        let app = XCUIApplication()
        setLanguage(app)
        app.launch()

        // In UI tests it’s important to set the initial state - such as interface orientation - required for your tests before they run. The setUp method is a good place to do this.
    }
    
    override func tearDown() {
        // Put teardown code here. This method is called after the invocation of each test method in the class.
        super.tearDown()
    }
    
    func testSwipe() {
        
        
        
    }
    
    func testExample() {
        // Use recording to get started writing UI tests.
        // Use XCTAssert and related functions to verify your tests produce the correct results.
        
        
        let app = XCUIApplication()
        let tablesQuery = app.tables
        tablesQuery.staticTexts["Index Path"].tap()
//        tablesQuery.cells.elementBoundByIndex(0).tap()
        
        snapshot("Without selection")
        
        tablesQuery.staticTexts["Section 0 Row 0"].tap()
        snapshot("1rowSelect")
        
        tablesQuery.staticTexts["Section 0 Row 1"].tap()
        snapshot("2rowSelect")
        
//        let image1 = UIImage(contentsOfFile: "/Users/mivanova/TestingProjects/UITesting/images/iPhone5-1.png")
//        let image2 = UIImage(contentsOfFile: "/Users/mivanova/TestingProjects/UITesting/screenshots/en-US/iPhone5-1rowSelect.png")
//        
//        let data1:NSData = UIImagePNGRepresentation(image1!)!
//        let data2:NSData = UIImagePNGRepresentation(image2!)!
//        
//        if(data1.isEqualToData(data2))
//        {
//            print("The same image")
//        }
//        else
//        {
//            print("Different image")
//            XCTAssert(data1.isEqualToData(data2), "Different image")
//        }
    }
    

    
}
