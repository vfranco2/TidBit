# Google News scraper class handles:
# -query to G News
# -extraction of key data from news articles
# -storage of scraped data in database
import requests
from bs4 import BeautifulSoup
import mysql.connector
#import MySQLdb


class GNewsScraper(object):
    img_size = '' #uncomment to return full-size image
    #img_size = '=pf-w400-h300' #Edit the 'w' and 'h' to re-size the returned image

    # Class constructor
    def __init__(self, query_category, max_articles):
        #self.__db = firebase_ref
        self.search_param = query_category
        self.response = ''
        self.soup = None

        #Determines the number of articles
        self.max_articles = max_articles

        self.article_URLs = []
        self.img_URLs = []
        self.article_headlines = []
        self.article_descriptions = []
        self.article_times_updated = []
        self.article_sources = []

    def extract_info(self):
         # Make Google News query
        try:
            self.response = requests.get('https://news.google.com/search?q='+self.search_param)
            self.soup = BeautifulSoup(self.response.text, 'html.parser')
            print("Fetching Google News results...")

            #Get first card on page
            card = self.soup.main.find('c-wiz').div.div
            #print("Getting first card on page...")

            # Extract max_articles number of articles and add their information to storage lists
            while len(self.img_URLs) < self.max_articles:

                #Get card figure tag (the news story photo);
                # if a card has no picture, skip it and scrape the next one
                card_figure = card.find("figure")
                if card_figure is not None:


                     #Find and pre-format photos returned from URLs
                    img_URL = card_figure.find('img')['src']             #Get URL from webpage
                    #print("Found card picture: ",img_URL)
                    if (img_URL.find('/attachments') != -1):             #If the returned image URL is a relative path,
                        img_URL = img_URL.replace('/attachments', 'https://news.google.com/attachments') #replace it with the absolute path

                    format_index = img_URL.index("=")                    #Find first index of the default pre-formating string
                    img_URL = img_URL[0:format_index]+self.img_size    #Replace the default string with format for our desired size

                    self.img_URLs.append(img_URL) # Finally, add the processed URL to the stored URL list

                    #Get card article tag
                    card_article = card.find("article")

                    # Replace the redirected Google News article with the original source URL
                    # Follow the acticle URL, and if it is redirected, save only the final destination URL
                    raw_article_URL = card_article.find("a")['href'].replace('.', 'https://news.google.com')
                    response = requests.get(raw_article_URL)
                    if response.history: #if the response was redirected:
                        self.article_URLs.append(response.url) #Save redirected URL
                    else:
                        print ("Article source URL request was not redirected")
                        self.article_URLs.append(response.url) #Save redirected URL


                    #Collect and store other article data
                    self.article_headlines.append(card_article.find('h3').find('a').get_text())
                    #print("Found article headline: ", self.article_headlines[0])
                    self.article_descriptions.append(card_article.find('h3').next_sibling.get_text())
                    #print("Found article description: ", self.article_descriptions[0])
                    self.article_times_updated.append(card_article.find('time').get_text())
                    #print("Found article time: ", self.article_times_updated[0])
                    self.article_sources.append(card_article.find('time').previous_sibling.get_text())
                    #print("Found article source: ", self.article_sources[0])


                else:
                    print("Card did not have a photo... skipping this card")

                #Increment the card (i.e. get the next card on the webpage)
                card = card.next_sibling

            print(self.search_param+": successfully scraped data!")
        except:
            print(self.search_param+" ERROR: could not scrape data.")

    def commit_to_database(self, mydb, catID):

        #--------------------------
        #Push article to database
        #--------------------------
        # try:
        #     #cursor is sql data element, create here
        #     cursor = mydb.cursor()
        #     #Update in table query
        #     #Update database with article fields retrieved from GNews. MUST BE IN ORDER OF APPEARANCE IN QUERY
        #     cursor.execute("UPDATE articles SET articleTitle ='%s', articleSource ='%s', articleImageUrl ='%s', articleText ='%s', articleUrl ='%s', articleDate ='%s' WHERE categoryId = %s"  \
        #     % (self.article_headlines[0], self.article_sources[0], self.img_URLs[0], self.article_descriptions[0], self.article_URLs[0], self.article_times_updated[0], catID))
        #     #commit new changes
        #     mydb.commit()
        #     #close cursor, keep data from overlapping
        #     cursor.close()
        #     print(f"CategoryID: {catID} successfully written to database!")
        # except:
        #     print(f"ERROR: CategoryID: {catID} not written to database")

        #cursor is sql data element, create here
        cursor = mydb.cursor()
        #Update in table query
        #Update database with article fields retrieved from GNews. MUST BE IN ORDER OF APPEARANCE IN QUERY
        cursor.execute("UPDATE articles SET articleTitle ='%s', articleSource ='%s', articleImageUrl ='%s', articleText ='%s', articleUrl ='%s', articleDate ='%s' WHERE categoryId = %s"  \
        % (self.article_headlines[0], self.article_sources[0], self.img_URLs[0], self.article_descriptions[0], self.article_URLs[0], self.article_times_updated[0], str(catID)))
        #commit new changes
        mydb.commit()
        #close cursor, keep data from overlapping
        cursor.close()
        return


    def print_to_console(self):
        print("Total articles scraped: ", len(self.img_URLs))
        # Print article data to terminal - FOR TEST/DEBUG PURPOSES
        for i in range(self.max_articles):
            print('Headline',i+1,':',self.article_headlines[i])
            print('Description',i+1,':',self.article_descriptions[i])
            print('Updated',i+1,':',self.article_times_updated[i])
            print('Source',i+1,':',self.article_sources[i])
            print('Article Link',i+1,':',self.article_URLs[i])
            print('Image URL',i+1,':',self.img_URLs[i])
            print('\n')
