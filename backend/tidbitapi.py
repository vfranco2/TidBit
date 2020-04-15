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
        dummyarticle = [{'id': article[0], 'categoryId': article[1], 'articleCount': article[2], 'articleTitle': article[3], 'articleSource': article[4], 'articleImageUrl': article[5], 'articleText': article[6], 'articleUrl': article[7], 'articleDate': article[8]}]
        listofarticles.append(dummyarticle)
        #return jsonify(dummyarticle)
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
