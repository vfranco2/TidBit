import flask
import mysql.connector
import configparser
import random
from flask import request, jsonify

app = flask.Flask(__name__)
app.config['DEBUG'] = True

#------------------
#Functions
#------------------
def parse_articles(result):
    listofarticles = []
    for article in result:
        articleTitle = article[3].replace('@', "'")
        articleSource = article[4].replace('@', "'")
        articleText = article[6].replace('@', "'")

        currentArticle = [{'id': article[0], 'categoryId': article[1], 'articleCount': article[2], 'articleTitle': articleTitle, 'articleSource': articleSource, 'articleImageUrl': article[5], 'articleText': articleText, 'articleUrl': article[7], 'articleDate': article[8]}]
        #dummyarticle[0] = dummyarticle[0].replace('@', "'")
        listofarticles.append(currentArticle)
    random.shuffle(listofarticles)
    return jsonify(listofarticles)

#------------------
#API Requests
#------------------
@app.route('/articles', methods=['GET'])
def home():
    config = configparser.ConfigParser()
    config.read('/home/vladfranco2/tidbitcfg/config.ini')

    mydb = mysql.connector.connect(
        host= config['data']['host'],
        user= config['data']['host'],
        passwd= config['data']['pass'],
        database = config['data']['db']
    )

    if 'categories' in request.args:
        queryCategories = str(request.args['categories'])
    else:
        return "Error: No categories provided."
    queryCategories = queryCategories.replace(' ', ',')
    cursor = mydb.cursor()
    cursor.execute("SELECT * FROM articles WHERE categoryId IN (%s)"  % (queryCategories))
    result = cursor.fetchall()
    mydb.close()

    return parse_articles(result)

app.run(host='0.0.0.0',port='5000')
