#!/usr/bin/env ruby

#require 'File'
require 'rubygems'
#require 'appium_lib'

#require 'testingbot'
gem "selenium-client"
gem "selenium-webdriver"
gem "touch_action"
require 'selenium/client'

require 'touch_action'
require 'selenium-webdriver'
require 'singleton'
require 'selenium/webdriver/common/action_builder'
#require 'page-object'

#include Selenium::WebDriver::DriverExtensions::HasTouchScreen


raise ArgumentError, "TEST_URL environment variable must be set" if ENV['TEST_URL'].nil?
raise ArgumentError, "MOBILE_PLATFORM environment variable must be set" if ENV['TEST_URL'].nil?
class TelerikMobileBot
    
    include Singleton
	

    
    attr_reader :driver, :downloasd_dir
    
    def remove_file(file)
        File.delete(file) if File.exists?(file)
    end
    
    def initialize
        
        @download_dir = File.join(Dir.pwd, "Selenium2", "resources")
        prefs = {"download" => {"default_directory" => @download_dir }}
        
        @rootUrl = ENV['TEST_URL']
        @mapFrom = Regexp.new(ENV['MAP_FROM'] || "(.*)")
        @mapTo = ENV['MAP_TO'] || '\1'
       
        @mobilePlatform = ENV['MOBILE_PLATFORM']
		
		puts "device name is", ENV['DEVICE_NAME']
		
        if @mobilePlatform.eql?('AndroidEmulator')
            desired_caps = Selenium::WebDriver::Remote::Capabilities.iphone       
            desired_caps['deviceName'] = ENV['DEVICE_NAME']
            desired_caps['platformName'] = 'Android'
            desired_caps['platformVersion'] = '5.0'
            desired_caps['browserName'] = 'Browser'
        end
        
        if @mobilePlatform.eql?('Android')
            desired_caps = Selenium::WebDriver::Remote::Capabilities.iphone       
            desired_caps['deviceName'] =  ENV['DEVICE_NAME']
            desired_caps['platformName'] = 'Android'
            desired_caps['platformVersion'] = '5.0'
            desired_caps['browserName'] = 'chrome'
        end
        
        if @mobilePlatform.eql?('iOS')
            desired_caps = Selenium::WebDriver::Remote::Capabilities.iphone
       
            desired_caps['deviceName'] = 'iPhone 5s'
            desired_caps['platformName'] = 'iOS'
            desired_caps['platformVersion'] = '7.1'
            desired_caps['browserName'] = 'safari'
        end
 
		#urlhub = "http://127.0.0.1:9515/wd/hub"
        urlhub = "http://127.0.0.1:4723/wd/hub"
        client = Selenium::WebDriver::Remote::Http::Default.new
        client.timeout = 120
       
       @driver = Selenium::WebDriver.for :remote,:url => urlhub , :desired_capabilities => desired_caps, :http_client => client
       #appium = Appium::Driver.new(caps: desired_caps)
       #@driver = appium.start_driver
    end
	

	
	def quit
        @driver.quit
    end
    
    def screenshot(test_case)
        $name_guid = rand.to_s[2..11]
        if test_case.nil?
            test_case = "null_#$name_guid"
        end
        $test_name = test_case
        $test_name.delete! '?\/.\`\': '
        $test_name = ($test_name.split('test', 2)[1]).slice(1..-1)
        Dir.mkdir("screenshots") if !File.directory?("screenshots")
        @driver.save_screenshot(File.join("screenshots", "#$test_name.jpg"))
    end
    
    def transform(url)
        url.gsub @mapFrom, @mapTo
    end
    
    def navigateToPage(url)
        @driver.navigate.to(@rootUrl + self.transform(url))
        sleep(2)
        rescue
        calling_test = caller[1]
        screenshot(calling_test)
    end
    
    def navigateAbsolute(url)
        @driver.navigate.to(self.transform(url))
        sleep(2)
        rescue
        calling_test = caller[1]
        screenshot(calling_test)
    end
    
	def flick(xpath)
		element = @driver.find_element(:xpath, xpath)
		driver.extend Selenium::WebDriver::DriverExtensions::HasTouchScreen
		driver.touch.flick(element,90,0, :normal).perform
	end
    
	
    def tap(xpath)
        begin
        element = @driver.find_element(:xpath, xpath)
        element.touch_action(:tap)
        #rescue NameError
        rescue
        end
    end
    
    def doubleTap(xpath)
        begin
        element = @driver.find_element(:xpath, xpath)
        element.touch_action(:doubletap)
        rescue
        end
    end
    
    def swipe(xpath)
         begin
         element = @driver.find_element(:xpath, xpath)
         #element.touch_action(:swipe, axis: 'x', distance: 500,  duration: 100)  
		 #element.touch_action(:flick, axis: 'x', distance: -200,  duration: 50) 
		 #element.touch_action(:flick, xdist: -700, ydist: 0, duration: 500)
		 element.touch_action(:press, hold: 3000) 
		 element.touch_action(:move, xdist: -170, ydist: 10,  duration: 500)
		 end
         rescue
    end
    
    def press(xpath)
        begin
            element = @driver.find_element(:xpath, xpath)
            element.touch_action(:press, hold: 2000)
            rescue
        end
    end
    
    def move(xpath)
        begin
            element = @driver.find_element(:xpath, xpath)
            element.touch_action(:move, xdist: -1200, ydist: 0,  duration: 500)
        rescue
	end
    
    end
    
    def refresh
        @driver.navigate.refresh
    end
    
 
        
    
    def get_text(xpath)
         element = @driver.find_element(:xpath, xpath)
         return element.text
    end
    
    def uploadFile(xpath, fileName)
        # element = @driver.find_element(:id, uploadId)
        element = @driver.find_element(:xpath, xpath)
        element.send_keys(fileName)
    end
		
		
	def click_and_hold(xpath)
	    element = driver.find_element(:xpath, xpath)
        element.click_and_hold
		rescue
		calling_test = caller[1]
		screenshot(calling_test)
	end
	
    def click(xpath)
        element = driver.find_element(:xpath, xpath)
        element.click
        rescue
        calling_test = caller[1]
        screenshot(calling_test)
    end
    
    def doubleClick(xpath)
        element = driver.find_element(:xpath, xpath)
        driver.action.double_click(element).perform
    end
    
    def present?(xpath)
        @driver.find_element(:xpath, xpath)
        true
        rescue Selenium::WebDriver::Error::NoSuchElementError
        #calling_test = caller[1]
        #screenshot(calling_test)
        false
    end
    
    def visible?(xpath)
        @driver.find_element(:xpath, xpath).displayed?
        rescue
        Selenium::WebDriver::Error::NoSuchElementError
        calling_test = caller[1]
        screenshot(calling_test)
        false
    end
    
    def notVisible?(xpath)
        !@driver.find_element(:xpath, xpath).displayed?
        rescue Selenium::WebDriver::Error::NoSuchElementError
        true
    end
    
    def waitForVisible(xpath)
        !7.times {
            break if (@driver.find_element(:xpath, xpath).displayed? rescue false)
            sleep 1
        }
    end
    
    def waitForElementNotPresent(xpath)
        !5.times {
            break if (@driver.find_element(:xpath, xpath).present? rescue true)
            sleep 1
        }
    end
    
    def waitForNotVisible(xpath)
        !5.times {
            break if (@driver.find_element(:xpath, xpath).visible? rescue true)
            sleep 1
        }
    end
    
    def attribute?(xpath, attr, val)
        !5.times {
            break if (@driver.find_element(:xpath, xpath).attribute(attr) == val rescue false);
            sleep 1
            false
        }
    end
    
    def type(xpath, text)
        @driver.find_element(:xpath, xpath).clear
        @driver.find_element(:xpath, xpath).send_keys text
        rescue
        calling_test = caller[1]
        screenshot(calling_test)
    end
    
    def switch_default
        @driver.switch_to.default_content
    end
    
    def switch_frame (xpath)
        @driver.switch_to.frame(@driver.find_element(:xpath, xpath))
    end
    
    def assertConfirmation(msg)
        # @driver.switch_to.alert.accept
        a = @driver.switch_to.alert
        if a.text == msg
            a.accept
            else
            a.accept
            assert(false, "Wrong alert text")
        end
    end
    
    def windowMaximize
        @driver.manage.window.maximize
    end
    
    def mouseOver(xpath)
        el = @driver.find_element(:xpath, xpath)
        driver.action.move_to(el).perform
    end
	
	def clickAndHold(xpath)
		el = @driver.find_element(:xpath, xpath)
		driver.action.click_and_hold(el).click.perform	
	end
	
	def release(xpath)
		el = @driver.find_element(:xpath, xpath)
		driver.action.click_and_hold(el).release.perform	
	end
    
    def typeKeys (xpath, keys)
        element = @driver.find_element(:xpath, xpath)
        element.clear
        element.send_keys keys
        element.send_keys :enter
        #rescue
        #	calling_test = caller[1]
        #	screenshot(calling_test)
    end
    
    def sendKeys (xpath, keys)
        @driver.find_element(:xpath, xpath).send_keys keys
    end
    
    def ctrlKeyDown
        @driver.action.key_down(:control).perform
    end
    
    def ctrlKeyUp
        @driver.action.key_up(:control).perform
    end
    
    def shiftKeyDown
        @driver.action.key_down(:shift).perform
    end
    
    def shiftKeyUp
        @driver.action.key_up(:shift).perform
    end
    
    def rightClick (xpath)
        element = driver.find_element(:xpath, xpath)
        @driver.action.context_click(element).perform
        rescue
        calling_test = caller[1]
        screenshot(calling_test)
    end
    
    
    def assertXpathCount(xpath, val)
        if @driver.find_elements(:xpath, xpath).size == val
            true
            else
            calling_test = caller[1]
            screenshot(calling_test)
            false
        end
    end
    
    def initializejQuery
        jQuerify =  "/*** dynamically load jQuery ***/
        (function(callback) {
         var JQUERY_URL = 'http://code.jquery.com/jquery-latest.min.js';
         if (typeof jQuery == 'undefined') {
         var script=document.createElement('script');
         script.src = JQUERY_URL;
         var head = document.getElementsByTagName('head')[0];
         var done = false;
         script.onload = script.onreadystatechange = (function() {
                                                      if (!done && (!this.readyState
                                                                    || this.readyState == 'loaded'
                                                                    || this.readyState == 'complete')) {
                                                      done = true;
                                                      script.onload = script.onreadystatechange = null;
                                                      callback(); // tell WebDriver we are done
                                                      head.removeChild(script);
                                                      }
                                                      });
         head.appendChild(script);
         }
         else {
         callback();
         }
         })(arguments[arguments.length - 1]);"
         
         # add jQuery to the current page
         @driver.execute_async_script(jQuerify)
         rescue
         calling_test = caller[1]
         screenshot(calling_test)
    end
    
    def assertEval (js, expectedValue)
        
        if expectedValue == @driver.execute_script(js)
            true
            else
            false
        end
    end
    
    def storeEval (js)
        $currentValue =  @driver.execute_script(js)
        return $currentValue		
    end
    
    def getEval (js)
        @driver.execute_script(js)
    end
    
    def assertAttribute (xpath,attr, val)
        !6.times{ break if (@driver.find_element(:xpath, xpath).attribute(attr) == val rescue false); sleep 1 }
    end
    
    def assertText (xpath, val)
        !6.times{ break if (@driver.find_element(:xpath, xpath).text.include?(val) == val rescue false); sleep 1 }
    end
    
    def dragAndDrop(source, target)
     
		dragThis = driver.find_element(:xpath, source) 
		dropHere = driver.find_element(:xpath, target)
		@driver.action.drag_and_drop(dragThis,dropHere).perform
		

	 #dragThis = driver.find_element(:xpath, source) 
        #dropHere = driver.find_element(:xpath, target)
    #    dragThis.touch_action(:press, hold: 3000).perform
		
		#@driver.action.drag_and_drop(dragThis,dropHere).perform	
		#dragThis.touch_action(:move, xdist: 0, ydist: -200,  duration: 3000).perform
       
	#dragThis.touch_action(:flick, axis: 'y', distance: -20,  duration: 50)
	   #dragThis.mouse.down
	#	   driver.mouse.move_to(dropHere).perform
	
	#driver.mouse.move_to(dropHere)
	#   dropHere.mouse.up
	   #dropHere.mouse.release
	  end
    
    def dragAndDropToCoords(source, x, y)
		
        dragThis = @driver.find_element(:xpath, source)
		#@driver.action.click_and_hold(dragThis).click.perform	
		#@driver.mouse.down dragThis
		#@driver.release
		#@driver.action.drag_and_drop(dragThis,dropHere).perform	
        #@driver.actions.move_by_offset(0, -200).perform
		#@driver.action.click_and_hold(dragThis).perform	
		
		#dragThis.touch_action(:move, { path: { xdist: x, ydist: y }, duration: 500 })
		#dragThis.touch_action(:flick, { axis: "y", distance: -200, duration: 50 })
		#@driver.action.drag_and_drop_by(dragThis, 0, 200).perform
		
		#swipeObject.put("startX", 0.95);
		#swipeObject.put("startY", 0.5);
		#swipeObject.put("endX", 0.05);
		#swipeObject.put("endY", 0.5);
		#swipeObject.put("duration", 1.8);
		#js.executeScript("mobile: swipe", swipeObject);
		#@driver.touch.scroll(dragThis, 0, -150)
		#@driver.execute_script 'mobile: swipe', :startX => 100, :startY => 100, :endX => 100, :endY => 300, :duration => 2
		#@driver.mouse.up		
		
		 #dragThis.touch_action(:press,  hold: 2000)

		#dragThis.touch_action(:move, xdist: 70, ydist: -50,  duration: 500).perform
  
		#dragThis.touch_action(:flick, axis: 'y', distance: 200,  duration: 50)
		#@driver.action.click_and_hold(dragThis).release.perform	
		sleep(1)
		@driver.touch.down(100, 100).perform
		sleep(1)
		@driver.touch.move(0, -100).perform
		sleep(1)
		@driver.touch.up(100, -100).perform
    end
    
    
    
    
end
