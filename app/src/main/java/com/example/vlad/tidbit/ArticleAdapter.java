package com.example.vlad.tidbit;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import android.content.Context;
import com.squareup.picasso.Picasso;
import java.util.List;

public class ArticleAdapter extends RecyclerView.Adapter<ArticleAdapter.ViewHolder>{
    private List<ArticleHolder> articleList;
    private Context context;

    //Card clicking stuff
    private OnItemClickListener mListener;

    public interface OnItemClickListener{
        void onItemClick(int position);
    }

    public void setOnItemClickListener(OnItemClickListener listener){
        mListener = listener;
    }

    //Viewholder
    public static class ViewHolder extends RecyclerView.ViewHolder{
        public TextView tvTitle, tvSource, tvContent, tvURL;
        public ImageView tvCategory, tvImage;

        public ViewHolder(View v, final OnItemClickListener listener){
            super(v);
            tvCategory = (ImageView)v.findViewById(R.id.card_category);
            tvTitle = (TextView)v.findViewById(R.id.title_text);
            tvSource = (TextView)v.findViewById(R.id.website_text);
            tvContent = (TextView)v.findViewById(R.id.article_text);
            tvImage = (ImageView)v.findViewById(R.id.article_image);
            tvURL = (TextView)v.findViewById(R.id.article_url);

            //Some item click stuff in constructor
            v.setOnClickListener(new View.OnClickListener(){
                @Override
                public void onClick(View v){
                    if (listener != null){
                        int position = getAdapterPosition();
                        if(position != RecyclerView.NO_POSITION){
                            listener.onItemClick(position);
                        }
                    }
                }
            });
        }
    }

    public ArticleAdapter(List<ArticleHolder> articleList,Context context){
        this.articleList = articleList;
        this.context = context;
    }

    // Yousif's function to check code working and see results
    public void printList(){
        System.out.print("[");
        for(ArticleHolder it : articleList) {
            System.out.print(it.toString() + ", ");
        }
        System.out.print("]");
    }

    @Override
    public ArticleAdapter.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType){
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.recyclerview_cards, parent, false);
        ViewHolder vh = new ViewHolder(v, mListener);
        return vh;
    }

    @Override
    public void onBindViewHolder(ArticleAdapter.ViewHolder holder, int position){
        ArticleHolder article = articleList.get(position);
        holder.tvCategory.setImageResource(article.getArticleCategory());
        holder.tvTitle.setText(article.getArticleTitle());
        holder.tvSource.setText(article.getWebsiteSource());
        holder.tvContent.setText(article.getArticleContent());

        holder.tvURL.setText(article.getArticleUrl());

        Picasso.with(context)
                .load(article.getArticleUrl())
                .resize(1500,750)
                .centerCrop()
                .error(R.drawable.tidbitbannerbw)
                .into(holder.tvImage);

    }

    @Override
    public int getItemCount(){
        return articleList.size();
    }
}