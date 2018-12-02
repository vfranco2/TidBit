package com.example.vlad.tidbit;

import java.util.ArrayList;
import java.util.List;

public class ArticleHolder {
    private String articleTitle, websiteSource, articleContent, articleURL, articleImageURL;
    int articleCategory;

    public ArticleHolder(int articleCategory, String articleTitle, String websiteSource, String articleContent, String articleURL, String articleImageUrl){
        this.articleCategory = articleCategory;
        this.articleTitle = articleTitle;
        this.websiteSource = websiteSource;
        this.articleContent = articleContent;
        this.articleImageURL = articleImageURL;
        this.articleURL = articleURL;
    }
    public int getArticleCategory(){
        return articleCategory;
    }
    public void setArticleCategory(int articleCategory){ this.articleCategory = articleCategory; }

    public String getArticleTitle(){
        return articleTitle;
    }
    public void setArticleTitle(String articleTitle){
        this.articleTitle = articleTitle;
    }

    public String getWebsiteSource(){
        return websiteSource;
    }
    public void setWebsiteSource(String websiteSource){
        this.websiteSource = websiteSource;
    }

    public String getArticleContent(){
        return articleContent;
    }
    public void setArticleContent(String articleContent){
        this.articleContent = articleContent;
    }

    public String getArticleImageURL(){
        return articleImageURL;
    }
    public void setArticleImageURL(String articleImageURL){
        this.articleImageURL = articleImageURL;
    }

    public String getArticleUrl(){
        return articleURL;
    }
    public void setArticleUrl(String articleURL){
        this.articleURL = articleURL;
    }
}