//
//  Tests.swift
//  UITesting
//
//  Created by Miroslava Ivanova on 1/12/16.
//  Copyright © 2016 Telerik. All rights reserved.
//

import Foundation
import XCTest

class Tests: XCTestCase {
    
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
    
    func testSelection() {
        // Use recording to get started writing UI tests.
        // Use XCTAssert and related functions to verify your tests produce the correct results.
        
        
        let tablesQuery = XCUIApplication().tables
        tablesQuery.staticTexts["Reorder"].tap()
//        tablesQuery.staticTexts["Apricot"].tap()
        tablesQuery.cells.elementBoundByIndex(0).tap()
        
        
        
        
        
    
    }
    
    
    
}
