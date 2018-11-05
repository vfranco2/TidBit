package com.example.vlad.tidbit;

import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    // Used to load the 'native-lib' library on application startup.
    static {
        System.loadLibrary("native-lib");
    }

    private DrawerLayout mDrawerLayout;

    private RecyclerView mRecyclerView;
    private RecyclerView.LayoutManager mLayoutManager;
    private List<ArticleHolder> articleList;
    private ArticleAdapter articleAdapter;

    String[] titles = {"Is Lebron the Goat?" , "Wow I love music" , "Space is Cool" , "More Languages" , "I Can Believe It's Not Butter"};
    String[] websource = {"ESPN" , "Pitchfork" , "Popular Science" , "Translate" , "Food Network"};
    String[] acontent = {"This is filler article content. Here's hoping it appears as it should on the card. I'm going to have a stroke if it doesn't, I swear. I've just about had it with these stubborn-ass cardviews and recyclerviews, shit irritating.", "This is filler article content. Here's hoping it appears as it should on the card. I'm going to have a stroke if it doesn't, I swear. I've just about had it with these stubborn-ass cardviews and recyclerviews, shit irritating.", "This is filler article content. Here's hoping it appears as it should on the card. I'm going to have a stroke if it doesn't, I swear. I've just about had it with these stubborn-ass cardviews and recyclerviews, shit irritating.", "This is filler article content. Here's hoping it appears as it should on the card. I'm going to have a stroke if it doesn't, I swear. I've just about had it with these stubborn-ass cardviews and recyclerviews, shit irritating.", "This is filler article content. Here's hoping it appears as it should on the card. I'm going to have a stroke if it doesn't, I swear. I've just about had it with these stubborn-ass cardviews and recyclerviews, shit irritating."};
    int[] images = {R.drawable.ic_menu, R.drawable.ic_menu,R.drawable.ic_menu,R.drawable.ic_menu,R.drawable.ic_menu};


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mDrawerLayout = findViewById(R.id.navmenubar);
        mRecyclerView = (RecyclerView) findViewById(R.id.recycler_view);
        mLayoutManager = new LinearLayoutManager(this);


        //Things that happen when you tap menu stuff
        NavigationView navigationView = findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(
                new NavigationView.OnNavigationItemSelectedListener() {
                    @Override
                    public boolean onNavigationItemSelected(MenuItem menuItem) {
                        // set item as selected to persist highlight
                        menuItem.setChecked(true);
                        // close drawer when item is tapped
                        mDrawerLayout.closeDrawers();

                        return true;
                    }
                });

        //Toolbar stuff
        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        ActionBar actionbar = getSupportActionBar();
        actionbar.setDisplayHomeAsUpEnabled(true);
        actionbar.setHomeAsUpIndicator(R.drawable.ic_menu);

        //Recycler view stuff
        mRecyclerView.setLayoutManager(mLayoutManager);
        articleList = new ArrayList<>();
        for (int i = 0; i < titles.length; i++){
            ArticleHolder article = new ArticleHolder(titles[i], websource[i], acontent[i], images[i]);
            articleList.add(article);
        }
        articleAdapter = new ArticleAdapter(articleList);
        mRecyclerView.setAdapter(articleAdapter);
        articleAdapter.notifyDataSetChanged();
    }

    //Handle drawer clicks
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                mDrawerLayout.openDrawer(GravityCompat.START);
                return true;
        }
        return super.onOptionsItemSelected(item);
    }

    //Handle recycler
    /*
    @Override
    public void onItemClick(View view, int position) {
        Toast.makeText(this, "You clicked " + adapter.getItem(position) + " on row number " + position, Toast.LENGTH_SHORT).show();
    }*/

}
