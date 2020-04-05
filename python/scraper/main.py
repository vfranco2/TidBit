# Main scraper file handles:
# -creation and initialization of scraper objects for ALL NEWS CATEGORIES
# -invoking methods to scrape news data and send to Firebase
from GNews import GNewsScraper
import firebase_admin
from firebase_admin import credentials, firestore

# Initialize Firebase SDK
try:
    cred = credentials.Certificate("./ServiceAccountKey.json")
    default_app = firebase_admin.initialize_app(cred)
    db = firestore.client()
except:
    print("main.py error: could not connect to Firebase.")

# Categories to feed into scraper
categories = [
        'gaming',
        'photography',
        'music',
        'sports',
        'cars',
        'movie',
        'finance',
        'cuisine',
        'fashion',
        'technology'    
]

# Scrape each category and store in Firebase
for category in categories:
    scraper = GNewsScraper(category,db)
    scraper.extract_info()
    scraper.send_to_firebase()