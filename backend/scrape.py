# Main scraper file handles:
# -creation and initialization of scraper objects for ALL NEWS CATEGORIES
# -invoking methods to scrape news data and send to database
from GNews import GNewsScraper
import configparser
import mysql.connector

#Sets the app-level maximum number of articles scraped PER CATEGORY
max_articles = 1

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
    user= config['data']['host'],
    passwd= config['data']['pass'],
    database = config['data']['db']
)

mydb.set_charset_collation('utf8mb4','utf8mb4_bin')

# Scrape each category and store in Firebase
for category in categories:
    scraper = GNewsScraper(category, max_articles)
    scraper.extract_info()
    scraper.print_to_console()
    #scraper.commit_to_database(mydb, categories.index(category))
    #scraper.print_to_console()

#close connection
mydb.close()
