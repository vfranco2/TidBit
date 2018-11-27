package com.example.vlad.tidbit;

import android.content.Intent;
import android.support.annotation.NonNull;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.MenuItem;
import android.net.*;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.firestore.*;
import com.google.android.gms.*;

import java.lang.Object;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import static java.util.logging.Logger.global;


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

    //Firebase references
    DocumentReference wordRef = FirebaseFirestore.getInstance().document("words/card_0");
    DocumentReference photosRef = FirebaseFirestore.getInstance().document("photography/card_0");
    DocumentReference carsRef = FirebaseFirestore.getInstance().document("cars/card_0");
    DocumentReference fashionRef = FirebaseFirestore.getInstance().document("fashion/card_0");
    DocumentReference movieRef = FirebaseFirestore.getInstance().document("movie/card_0");
    DocumentReference financeRef = FirebaseFirestore.getInstance().document("finance/card_0");
    DocumentReference foodRef = FirebaseFirestore.getInstance().document("food/card_0");
    DocumentReference musicRef = FirebaseFirestore.getInstance().document("music/card_0");
    DocumentReference sportsRef = FirebaseFirestore.getInstance().document("sports/card_0");
    DocumentReference techRef = FirebaseFirestore.getInstance().document("technology/card_2");

    private DocumentReference[] firebaseDocs = {
            wordRef,
            photosRef,
            sportsRef,
            carsRef,
            fashionRef,
            movieRef,
            financeRef,
            foodRef,
            musicRef,
            techRef
    };

    //Used for getting data fields in Firebase
    public static final String TAG = "TIDBIT";
    public static final String TIDBIT_KEY = "tidbit";
    public static final String SOURCE_KEY = "source";
    public static final String HEADLINE_KEY = "headline";
    public static final String IMAGE_KEY = "image_URL";
    public static final String URL_KEY = "article_link";

    public static final String[] CATEGORIES = {
            "WOD",
            "POD",
            "sports",
            "cars",
            "fashion",
            "movie",
            "finance",
            "food",
            "music",
            "technology"};
    int[] category = {R.drawable.ic_action_emo_laugh,
            R.drawable.ic_action_camera,
            R.drawable.ic_action_ball,
            R.drawable.ic_action_car,
            R.drawable.ic_action_glasses,
            R.drawable.ic_action_movie,
            R.drawable.ic_action_line_chart,
            R.drawable.ic_action_restaurant,
            R.drawable.ic_action_record,
            R.drawable.ic_action_laptop};
    String[] titles = {"Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!"};
    String[] websource = {"", "", "", "", "", "", "", "", "", "",};
    static String[] acontent = {"Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!",
            "Content not found!"};
    int[] images = {R.drawable.word,
            R.drawable.flickr,
            R.drawable.lebron,
            R.drawable.miata,
            R.drawable.yeezy,
            R.drawable.imdb,
            R.drawable.stock,
            R.drawable.food,
            R.drawable.music,
            R.drawable.server};
    String[] source_URLs = {"", "", "", "", "", "", "", "", "", "",};

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mDrawerLayout = findViewById(R.id.navmenubar);
        mRecyclerView = findViewById(R.id.recycler_view);
        mLayoutManager = new LinearLayoutManager(this);

        //Drawer menu selection
        NavigationView navigationView = findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(
                new NavigationView.OnNavigationItemSelectedListener() {
                    @Override
                    public boolean onNavigationItemSelected(MenuItem menuItem) {
                        // set item as selected to persist highlight
                        menuItem.setChecked(true);
                        switch(menuItem.getItemId()){
                            case R.id.nav_interests:
                                Intent interestsIntent = new Intent(MainActivity.this, ActivityInterests.class);
                                startActivity(interestsIntent);
                                break;
                            case R.id.nav_favorites:
                                Intent favoritesIntent = new Intent(MainActivity.this, ActivityFavorites.class);
                                startActivity(favoritesIntent);
                                break;
                            case R.id.nav_account:
                                Intent accountIntent = new Intent(MainActivity.this, ActivityAccount.class);
                                startActivity(accountIntent);
                                break;
                            case R.id.nav_settings:
                                Intent settingsIntent = new Intent(MainActivity.this, ActivitySettings.class);
                                startActivity(settingsIntent);
                                break;
                        }

                        // close drawer when item is tapped
                        mDrawerLayout.closeDrawers();
                        return true;
                    }
                });

        //Toolbar stuff
        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        toolbar.setTitleTextColor(android.graphics.Color.WHITE);
        ActionBar actionbar = getSupportActionBar();
        actionbar.setDisplayHomeAsUpEnabled(true);
        actionbar.setHomeAsUpIndicator(R.drawable.ic_menu);

        //Recycler view stuff, fills with default images/text
        mRecyclerView.setLayoutManager(mLayoutManager);
        articleList = new ArrayList<>();
        for (int i = 0; i < titles.length; i++){
            ArticleHolder article = new ArticleHolder(category[i], titles[i], websource[i], acontent[i], images[i], source_URLs[i]);
            articleList.add(article);
        }
        articleAdapter = new ArticleAdapter(articleList);
        mRecyclerView.setAdapter(articleAdapter);
        articleAdapter.notifyDataSetChanged();

        //Firebase stuff, assigns scraped data to cards
        for (int k = 0; k <firebaseDocs.length; k++){
            final int j = k;
            firebaseDocs[j].get().addOnSuccessListener(new OnSuccessListener<DocumentSnapshot>() {
                @Override
                public void onSuccess(DocumentSnapshot documentSnapshot) {
                    if (documentSnapshot.exists()){
                        acontent[j] = documentSnapshot.getString(TIDBIT_KEY);
                        websource[j] = documentSnapshot.getString(SOURCE_KEY);
                        titles[j] = documentSnapshot.getString(HEADLINE_KEY);
                        source_URLs[j] = documentSnapshot.getString(URL_KEY);
                        Log.d(TAG, "Document was successfully retrieved: "+websource[3]+": "+acontent[3]);

                        //Re-populate the view with database content at that location
                        ArticleHolder article = new ArticleHolder(category[j], titles[j], websource[j], acontent[j], images[j], source_URLs[j]);
                        articleList.set(j, article);

                        articleAdapter = new ArticleAdapter(articleList);
                        mRecyclerView.setAdapter(articleAdapter);
                        articleAdapter.notifyDataSetChanged();

                        //Click handling for each article
                        articleAdapter.setOnItemClickListener(new ArticleAdapter.OnItemClickListener() {
                            @Override
                            public void onItemClick(int position) {
                                Uri webpage = Uri.parse(source_URLs[position]);
                                Toast.makeText(getApplicationContext(), "You clicked " + position, Toast.LENGTH_SHORT).show();
                                Intent intent = new Intent(Intent.ACTION_VIEW, webpage);
                                if (intent.resolveActivity(getPackageManager()) != null) {
                                    startActivity(intent);
                                }
                            }
                        });
                    }
                    else {
                        Log.d(TAG, "Error: Document does not exist.");
                    }
                }
            }).addOnFailureListener(new OnFailureListener() {
                @Override
                public void onFailure(@NonNull Exception e) {
                    Log.d(TAG, "Document was not retrieved.", e);
                }
            });
        }

    }

    //Handle drawer click opening
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                mDrawerLayout.openDrawer(GravityCompat.START);
                return true;
        }
        return super.onOptionsItemSelected(item);
    }

}