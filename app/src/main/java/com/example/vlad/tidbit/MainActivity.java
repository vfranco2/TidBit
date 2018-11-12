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
import java.lang.Object;
import java.util.ArrayList;
import java.util.Arrays;
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

    int[] category = {R.drawable.ic_action_emo_laugh,
            R.drawable.ic_action_camera,
            R.drawable.ic_action_ball,
            R.drawable.ic_action_car,
            R.drawable.ic_action_glasses,
            R.drawable.ic_action_movie,
            R.drawable.ic_action_line_chart,
            R.drawable.ic_action_restaurant,
            R.drawable.ic_action_news,
            R.drawable.ic_action_record,
            R.drawable.ic_action_laptop,
            R.drawable.ic_action_globe};
    String[] titles = {"Word of the Day:",
            "Today's Photo:",
            "Sports Article",
            "Auto Article",
            "Fashion Article",
            "Movie Article",
            "Finance Article",
            "Food Article",
            "Local Article",
            "Music Article",
            "Tech Article",
            "World Article"};
    String[] websource = {"Dictionary",
            "Flickr",
            "ESPN",
            "Jalopnik",
            "StockX",
            "IMDB",
            "Bloomberg",
            "Food Network",
            "OU News",
            "Pitchfork",
            "Wired",
            "CNN"};
    static String[] acontent = {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur in odio neque. Fusce convallis mauris at dui lacinia tincidunt. Etiam ex augue, posuere at velit sed, imperdiet vehicula metus. Aliquam eu maximus erat. Quisque nec libero pellentesque, egestas arcu et, luctus odio. Nulla tincidunt massa urna, nec sodales lacus ultricies vel.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur in odio neque. Fusce convallis mauris at dui lacinia tincidunt. Etiam ex augue, posuere at velit sed, imperdiet vehicula metus. Aliquam eu maximus erat. Quisque nec libero pellentesque, egestas arcu et, luctus odio. Nulla tincidunt massa urna, nec sodales lacus ultricies vel.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur in odio neque. Fusce convallis mauris at dui lacinia tincidunt. Etiam ex augue, posuere at velit sed, imperdiet vehicula metus. Aliquam eu maximus erat. Quisque nec libero pellentesque, egestas arcu et, luctus odio. Nulla tincidunt massa urna, nec sodales lacus ultricies vel.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur in odio neque. Fusce convallis mauris at dui lacinia tincidunt. Etiam ex augue, posuere at velit sed, imperdiet vehicula metus. Aliquam eu maximus erat. Quisque nec libero pellentesque, egestas arcu et, luctus odio. Nulla tincidunt massa urna, nec sodales lacus ultricies vel.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur in odio neque. Fusce convallis mauris at dui lacinia tincidunt. Etiam ex augue, posuere at velit sed, imperdiet vehicula metus. Aliquam eu maximus erat. Quisque nec libero pellentesque, egestas arcu et, luctus odio. Nulla tincidunt massa urna, nec sodales lacus ultricies vel.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur in odio neque. Fusce convallis mauris at dui lacinia tincidunt. Etiam ex augue, posuere at velit sed, imperdiet vehicula metus. Aliquam eu maximus erat. Quisque nec libero pellentesque, egestas arcu et, luctus odio. Nulla tincidunt massa urna, nec sodales lacus ultricies vel.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur in odio neque. Fusce convallis mauris at dui lacinia tincidunt. Etiam ex augue, posuere at velit sed, imperdiet vehicula metus. Aliquam eu maximus erat. Quisque nec libero pellentesque, egestas arcu et, luctus odio. Nulla tincidunt massa urna, nec sodales lacus ultricies vel.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur in odio neque. Fusce convallis mauris at dui lacinia tincidunt. Etiam ex augue, posuere at velit sed, imperdiet vehicula metus. Aliquam eu maximus erat. Quisque nec libero pellentesque, egestas arcu et, luctus odio. Nulla tincidunt massa urna, nec sodales lacus ultricies vel.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur in odio neque. Fusce convallis mauris at dui lacinia tincidunt. Etiam ex augue, posuere at velit sed, imperdiet vehicula metus. Aliquam eu maximus erat. Quisque nec libero pellentesque, egestas arcu et, luctus odio. Nulla tincidunt massa urna, nec sodales lacus ultricies vel.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur in odio neque. Fusce convallis mauris at dui lacinia tincidunt. Etiam ex augue, posuere at velit sed, imperdiet vehicula metus. Aliquam eu maximus erat. Quisque nec libero pellentesque, egestas arcu et, luctus odio. Nulla tincidunt massa urna, nec sodales lacus ultricies vel.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur in odio neque. Fusce convallis mauris at dui lacinia tincidunt. Etiam ex augue, posuere at velit sed, imperdiet vehicula metus. Aliquam eu maximus erat. Quisque nec libero pellentesque, egestas arcu et, luctus odio. Nulla tincidunt massa urna, nec sodales lacus ultricies vel.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur in odio neque. Fusce convallis mauris at dui lacinia tincidunt. Etiam ex augue, posuere at velit sed, imperdiet vehicula metus. Aliquam eu maximus erat. Quisque nec libero pellentesque, egestas arcu et, luctus odio. Nulla tincidunt massa urna, nec sodales lacus ultricies vel."
    };

    int[] images = {R.drawable.word,
            R.drawable.flickr,
            R.drawable.lebron,
            R.drawable.miata,
            R.drawable.yeezy,
            R.drawable.imdb,
            R.drawable.stock,
            R.drawable.food,
            R.drawable.ou,
            R.drawable.music,
            R.drawable.server,
            R.drawable.worldnews};


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mDrawerLayout = findViewById(R.id.navmenubar);
        mRecyclerView = (RecyclerView) findViewById(R.id.recycler_view);
        mLayoutManager = new LinearLayoutManager(this);


        //Drawer menu selection
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
            ArticleHolder article = new ArticleHolder(category[i], titles[i], websource[i], acontent[i], images[i]);
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
