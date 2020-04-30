# Main scraper file handles:
# -creation and initialization of scraper objects for ALL NEWS CATEGORIES
# -invoking methods to scrape news data and send to database
from GNews import GNewsScraper
import configparser
import mysql.connector
import datetime

#Sets the app-level maximum number of articles scraped PER CATEGORY
max_articles = 2

# Categories to feed into scraper
# TODO: re-arrange in correct order for categoryID to store the correct categories..
categories = [
        'technology',
        'cars',
        'sports',
        'fashion',
        'gaming',
        'movie',
        'cuisine',
        'music',
        'photography',
        'finance'
]

#Initialize DB connection, add credentials
config = configparser.ConfigParser()
config.read('/home/vladfranco2/tidbitcfg/config.ini')

mydb = mysql.connector.connect(
    host= config['data']['host'],
    user= config['data']['user'],
    passwd= config['data']['pass'],
    database = config['data']['db']
)

mydb.set_charset_collation('utf8mb4','utf8mb4_bin')

#For logging purposes
dt = datetime.datetime.now()
print('#'*100)
print(f'Begin article scraping: {dt.strftime("%c")}')
print('#'*100)

# Scrape each category and store in Firebase
for category in categories:
    scraper = GNewsScraper(category, max_articles)
    scraper.extract_info()
    scraper.print_to_console()
    scraper.commit_to_database(mydb, categories.index(category))

#close connection
mydb.close()

dt = datetime.datetime.now()
print('#'*100)
print(f'Ended article scraping: {dt.strftime("%c")}')
print('#'*100)
print('\n')
