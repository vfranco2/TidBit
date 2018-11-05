package com.example.vlad.tidbit;

import java.util.ArrayList;
import java.util.List;

public class ArticleHolder {
    private String articleTitle, websiteSource, articleContent;
    int articleImage;

    public ArticleHolder(String articleTitle, String websiteSource, String articleContent, int articleImage){
        this.articleTitle = articleTitle;
        this.websiteSource = websiteSource;
        this.articleContent = articleContent;
        this.articleImage = articleImage;
    }

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


}