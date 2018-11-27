package com.example.vlad.tidbit;

import java.util.ArrayList;
import java.util.List;

public class ArticleHolder {
    private String articleTitle, websiteSource, articleContent, articleURL;
    int articleCategory, articleImage;

    public ArticleHolder(int articleCategory, String articleTitle, String websiteSource, String articleContent, int articleImage, String articleURL){
        this.articleCategory = articleCategory;
        this.articleTitle = articleTitle;
        this.websiteSource = websiteSource;
        this.articleContent = articleContent;
        this.articleImage = articleImage;
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

    public int getArticleImage(){
        return articleImage;
    }
    public void setArticleImage(int articleImage){
        this.articleImage = articleImage;
    }

    public String getArticleUrl(){
        return articleURL;
    }
    public void setArticleUrl(String articleURL){
        this.articleURL = articleURL;
    }
}