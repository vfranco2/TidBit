package com.example.vlad.tidbit;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.List;

public class ArticleAdapter extends RecyclerView.Adapter<ArticleAdapter.ViewHolder>{
    private List<ArticleHolder> articleList;

    public static class ViewHolder extends RecyclerView.ViewHolder{
        public TextView tvTitle, tvSource, tvContent;
        public ImageView tvImage;

        public ViewHolder(View v){
            super(v);
            tvTitle = (TextView)v.findViewById(R.id.title_text);
            tvSource = (TextView)v.findViewById(R.id.website_text);
            tvContent = (TextView)v.findViewById(R.id.article_text);
            tvImage = (ImageView)v.findViewById(R.id.article_image);
        }
    }

    public ArticleAdapter(List<ArticleHolder> articleList){
        this.articleList = articleList;
    }

    @Override
    public ArticleAdapter.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType){
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.recyclerview_cards, parent, false);
        ViewHolder vh = new ViewHolder(v);
        return vh;
    }

    @Override
    public void onBindViewHolder(ArticleAdapter.ViewHolder holder, int position){
        ArticleHolder article = articleList.get(position);
        holder.tvTitle.setText(article.getArticleTitle());
        holder.tvSource.setText(article.getWebsiteSource());
        holder.tvContent.setText(article.getArticleContent());
        holder.tvImage.setImageResource(article.getArticleImage());
    }

    @Override
    public int getItemCount(){
        return articleList.size();
    }
}
