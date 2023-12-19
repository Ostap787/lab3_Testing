Feature: Binance API Verification

Scenario: Verify Binance API Recent Trades
    Given I have a valid Binance API endpoint 'https://api.binance.com'
    When I make a GET request to the Binance API for recent trades
    Then I should receive a successful response
    And the response should contain recent trades information





