#!/usr/bin/env ruby

require 'selenium-webdriver'
require 'minitest'
require 'minitest/autorun'
require_relative 'mobileBot'


class MobileTests < MiniTest::Test
    
    def setup
        @bot = TelerikMobileBot.instance
    end
  
    def test_action_sheet_reply
        @bot.navigateToPage("m/index#actionsheet/index")
        @bot.waitForVisible("//span[.='Reply']")
		sleep(1)
        @bot.tap("//span[.='Reply']")
        sleep(2)
        assert(@bot.visible?("//ul[@id='inboxActions']"), "Element is visible");
        @bot.tap("//div[@class='k-animation-container']//a[.='Reply']")
        @bot.waitForNotVisible("//*[@id='inboxActions']")
        assert(!@bot.visible?("//*[@id='inboxActions']"), "Element is not visible");
    end
    
    
end
