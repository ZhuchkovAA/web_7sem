import time
from selenium import webdriver
import undetected_chromedriver as uc
from selenium.webdriver.common.by import By

class Parser:
    def __init__(self):
        self.url = 'https://quiz-on.ru/#/'
        self.driver = self.gen_driver()

    def gen_driver(self):
        chrome_options = uc.ChromeOptions()
        chrome_options.add_argument("--start-maximized")
        chrome_options.page_load_strategy = 'none'
        driver = uc.Chrome(options=chrome_options)
        driver.implicitly_wait(6)
        return driver

    def reg_quizon(self):
        to_reg_btn = None
        while (not to_reg_btn):
            try:
                self.driver.get(self.url)
                to_reg_btn = self.driver.find_element(By.CLASS_NAME, 'reg-button')
                if to_reg_btn.get_attribute('disabled'): 
                    to_reg_btn = None
                    time.sleep(3)
                    self.driver.refresh()
                else:
                    to_reg_btn.click()
            except:
                self.driver.refresh()

        time.sleep(1)

        captain_name = self.driver.find_element(By.ID, 'captain_name')
        captain_name.send_keys('Гизетдинова Полина Александровна')

        group_name = self.driver.find_element(By.ID, 'group_name')
        group_name.send_keys('ИБМ2-71Б')

        phone = self.driver.find_element(By.ID, 'phone')
        phone.click()
        phone.send_keys('9613759477')

        telegram = self.driver.find_element(By.ID, 'telegram')
        telegram.send_keys('@pollyskater')

        team_name = self.driver.find_element(By.ID, 'team_name')
        team_name.send_keys('Легендарный мох')

        team_id = self.driver.find_element(By.ID, 'team_id')
        team_id.send_keys('0015')

        players_amount = self.driver.find_element(By.ID, 'players_amount')
        players_amount.send_keys('8')

        reg_btn = self.driver.find_element(By.CLASS_NAME, 'reg-btn')
        # to_reg_btn.click()

        time.sleep(200)
    
if __name__ == "__main__":
    parser = Parser()
    parser.reg_quizon()