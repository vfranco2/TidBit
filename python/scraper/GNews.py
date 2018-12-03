# Google News scraper class handles:
# -query to G News
# -extraction of key data from news articles
# -storage of scraped data in Firebase
import requests
from bs4 import BeautifulSoup
import firebase_admin
from firebase_admin import credentials, firestore


class GNewsScraper(object):
    #Firebase references.
    __db = None

    #Class-level scraper vars.
    __search_param = ''
    __response = ''
    __soup = None

    # Class-level article data lists & vars.
    __max_articles = 5 
    
    __article_URLs = []
    __img_URLs = []
    __img_size = '' #uncomment to return full-size image
    #__img_size = '=pf-w400-h300' #Edit the 'w' and 'h' to re-size the returned image
    __article_headlines = []
    __article_descriptions = []
    __article_times_updated = []
    __article_sources = []

    # Class constructor
    def __init__(self, query, firebase_ref):
        self.__db = firebase_ref
        self.__search_param = query
        self.__response = ''
        self.__soup = None

       
        self.__article_URLs = []
        self.__img_URLs = []       
        self.__article_headlines = []
        self.__article_descriptions = []
        self.__article_times_updated = []
        self.__article_sources = []
    
    def extract_info(self):
         # Make Google News query 
        try:
            self.__response = requests.get('https://news.google.com/search?q='+self.__search_param)
            self.__soup = BeautifulSoup(self.__response.text, 'html.parser')

            #Get first card on page
            card = self.__soup.main.find('c-wiz').div.div

            # Extract article data and add to lists
            while len(self.__img_URLs) <= self.__max_articles:

                #Get card figure tag
                card_figure = card.find("figure")
                if card_figure is not None:

                     #Find and pre-format photos returned from URLs
                    img_URL = card_figure.find('img')['src']             #Get URL from webpage
                    if (img_URL.find('/attachments') != -1):             #If the returned image URL is a relative path,
                        img_URL = img_URL.replace('/attachments', 'https://news.google.com/attachments') #replace it with the absolute path

                    format_index = img_URL.index("=")                    #Find first index of the default pre-formating string
                    img_URL = img_URL[0:format_index]+self.__img_size    #Replace the default string with format for our desired size
            
                    self.__img_URLs.append(img_URL) # Finally, add the processed URL to the stored URL list

                    #Get card article tag
                    card_article = card.find("article")
                    #Follow the acticle URL, and if it is redirected, save only the final destination URL
                    raw_article_URL = card_article.find("a")['href'].replace('.', 'https://news.google.com')
                    response = requests.get(raw_article_URL)
                    if response.history: #if the response was redirected:
                        self.__article_URLs.append(response.url) #Save redirected URL
                    else:
                        print ("Article source URL request was not redirected")
                        self.__article_URLs.append(response.url) #Save redirected URL

                    #Collect and store other article data
                    self.__article_headlines.append(card_article.find('h3').find('a').get_text())
                    self.__article_descriptions.append(card_article.find("p").get_text())
                    self.__article_times_updated.append(card_article.find('time').get_text())
                    self.__article_sources.append(card_article.find('time').previous_sibling.get_text())

                else:
                    print("Card did not have a photo... skipping this card")
                
                #Increment the card (i.e. get the next card on the webpage)
                card = card.next_sibling
        
            print(self.__search_param+": successfully scraped data!")
        except:
            print(self.__search_param+" ERROR: could not scrape data.")



    def send_to_firebase(self):
        try:
            # Send all categories to Firebase in one batch upload
            batch = self.__db.batch()
            for i in range (self.__max_articles):
                current_card = 'card_'+str(i)
                doc_ref = self.__db.collection(self.__search_param).document(current_card)
                batch.set(doc_ref, {
                    u'headline': self.__article_headlines[i],
                    u'source': self.__article_sources[i],
                    u'last_updated': self.__article_times_updated[i],
                    u'tidbit': self.__article_descriptions[i],
                    u'article_link': self.__article_URLs[i],
                    u'image_URL': self.__img_URLs[i],
                })
            # Commit the batch and send all the cards to Firebase at once
            batch.commit()
            print(self.__search_param+": successfully written to Firebase!") 
        except:
            print(self.__search_param+" ERROR: could not write to Firebase.")



    def print_to_console(self):
        # Print article data - FOR TEST PURPOSES ONLY
        for i in range(self.__max_articles):
            print('Headline',i+1,':',self.__article_headlines[i])
            print('Description',i+1,':',self.__article_descriptions[i])
            print('Updated',i+1,':',self.__article_times_updated[i])
            print('Source',i+1,':',self.__article_sources[i])
            print('Article Link',i+1,':',self.__article_URLs[i])
            print('Image URL',i+1,':',self.__img_URLs[i])
            print('\n')