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
            "technology"
    };

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
    String[] titles = {"Word of the Day:Word of the Day:Word of the Day:Word of the Day:Word of the Day:",
            "Today's Photo:",
            "Sports Article",
            "Auto Article",
            "Fashion Article",
            "Movie Article",
            "Finance Article",
            "Food Article",
            "Music Article",
            "Tech Article"};
    String[] websource = {"Dictionary",
            "Flickr",
            "ESPN",
            "Jalopnik",
            "StockX",
            "IMDB",
            "Bloomberg",
            "Food Network",
            "Pitchfork",
            "Wired"};
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
            };
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
                        switch(menuItem.getItemId()){
                            case R.id.nav_interests:
                                Intent interestsIntent = new Intent(MainActivity.this, ActivityInterests.class);
                                startActivity(interestsIntent);
                                break;
                            case R.id.nav_favorites:
                                Intent interestsFavorites = new Intent(MainActivity.this, ActivityFavorites.class);
                                startActivity(interestsFavorites);
                                break;
                            case R.id.nav_account:
                                Intent interestsAccount = new Intent(MainActivity.this, ActivityAccount.class);
                                startActivity(interestsAccount);
                                break;
                            case R.id.nav_settings:
                                Intent interestsSettings = new Intent(MainActivity.this, ActivitySettings.class);
                                startActivity(interestsSettings);
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
        // -------------------------------------------------------------------------
        // -------------------------------------------------------------------------
        // -------------------------------------------------------------------------
        //Firebase stuff
        firebaseDocs[0].get().addOnSuccessListener(new OnSuccessListener<DocumentSnapshot>() {
            @Override
            public void onSuccess(DocumentSnapshot documentSnapshot) {
                if (documentSnapshot.exists()){
                    acontent[0] = documentSnapshot.getString(TIDBIT_KEY);
                    websource[0] = documentSnapshot.getString(SOURCE_KEY);
                    titles[0] = documentSnapshot.getString(HEADLINE_KEY);
                    Log.d(TAG, "Document was successfully retrieved: "+websource[3]+": "+acontent[3]);

                    //Re-populate the view with database content
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

        firebaseDocs[1].get().addOnSuccessListener(new OnSuccessListener<DocumentSnapshot>() {
            @Override
            public void onSuccess(DocumentSnapshot documentSnapshot) {
                if (documentSnapshot.exists()){
                    acontent[1] = documentSnapshot.getString(TIDBIT_KEY);
                    websource[1] = documentSnapshot.getString(SOURCE_KEY);
                    titles[1] = documentSnapshot.getString(HEADLINE_KEY);
                    Log.d(TAG, "Document was successfully retrieved: "+websource[3]+": "+acontent[3]);

                    //Re-populate the view with database content
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

        firebaseDocs[2].get().addOnSuccessListener(new OnSuccessListener<DocumentSnapshot>() {
            @Override
            public void onSuccess(DocumentSnapshot documentSnapshot) {
                if (documentSnapshot.exists()){
                    acontent[2] = documentSnapshot.getString(TIDBIT_KEY);
                    websource[2] = documentSnapshot.getString(SOURCE_KEY);
                    titles[2] = documentSnapshot.getString(HEADLINE_KEY);
                    Log.d(TAG, "Document was successfully retrieved: "+websource[3]+": "+acontent[3]);

                    //Re-populate the view with database content
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


        firebaseDocs[3].get().addOnSuccessListener(new OnSuccessListener<DocumentSnapshot>() {
            @Override
            public void onSuccess(DocumentSnapshot documentSnapshot) {
                if (documentSnapshot.exists()){
                    acontent[3] = documentSnapshot.getString(TIDBIT_KEY);
                    websource[3] = documentSnapshot.getString(SOURCE_KEY);
                    titles[3] = documentSnapshot.getString(HEADLINE_KEY);
                    Log.d(TAG, "Document was successfully retrieved: "+websource[3]+": "+acontent[3]);

                    //Re-populate the view with database content
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

        firebaseDocs[4].get().addOnSuccessListener(new OnSuccessListener<DocumentSnapshot>() {
            @Override
            public void onSuccess(DocumentSnapshot documentSnapshot) {
                if (documentSnapshot.exists()){
                    acontent[4] = documentSnapshot.getString(TIDBIT_KEY);
                    websource[4] = documentSnapshot.getString(SOURCE_KEY);
                    titles[4] = documentSnapshot.getString(HEADLINE_KEY);
                    Log.d(TAG, "Document was successfully retrieved: "+websource[3]+": "+acontent[3]);

                    //Re-populate the view with database content
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

        firebaseDocs[5].get().addOnSuccessListener(new OnSuccessListener<DocumentSnapshot>() {
            @Override
            public void onSuccess(DocumentSnapshot documentSnapshot) {
                if (documentSnapshot.exists()){
                    acontent[5] = documentSnapshot.getString(TIDBIT_KEY);
                    websource[5] = documentSnapshot.getString(SOURCE_KEY);
                    titles[5] = documentSnapshot.getString(HEADLINE_KEY);
                    Log.d(TAG, "Document was successfully retrieved: "+websource[3]+": "+acontent[3]);

                    //Re-populate the view with database content
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

        firebaseDocs[6].get().addOnSuccessListener(new OnSuccessListener<DocumentSnapshot>() {
            @Override
            public void onSuccess(DocumentSnapshot documentSnapshot) {
                if (documentSnapshot.exists()){
                    acontent[6] = documentSnapshot.getString(TIDBIT_KEY);
                    websource[6] = documentSnapshot.getString(SOURCE_KEY);
                    titles[6] = documentSnapshot.getString(HEADLINE_KEY);
                    Log.d(TAG, "Document was successfully retrieved: "+websource[3]+": "+acontent[3]);

                    //Re-populate the view with database content
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

        firebaseDocs[7].get().addOnSuccessListener(new OnSuccessListener<DocumentSnapshot>() {
            @Override
            public void onSuccess(DocumentSnapshot documentSnapshot) {
                if (documentSnapshot.exists()){
                    acontent[7] = documentSnapshot.getString(TIDBIT_KEY);
                    websource[7] = documentSnapshot.getString(SOURCE_KEY);
                    titles[7] = documentSnapshot.getString(HEADLINE_KEY);
                    Log.d(TAG, "Document was successfully retrieved: "+websource[3]+": "+acontent[3]);

                    //Re-populate the view with database content
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

        firebaseDocs[8].get().addOnSuccessListener(new OnSuccessListener<DocumentSnapshot>() {
            @Override
            public void onSuccess(DocumentSnapshot documentSnapshot) {
                if (documentSnapshot.exists()){
                    acontent[8] = documentSnapshot.getString(TIDBIT_KEY);
                    websource[8] = documentSnapshot.getString(SOURCE_KEY);
                    titles[8] = documentSnapshot.getString(HEADLINE_KEY);
                    Log.d(TAG, "Document was successfully retrieved: "+websource[3]+": "+acontent[3]);

                    //Re-populate the view with database content
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


        firebaseDocs[9].get().addOnSuccessListener(new OnSuccessListener<DocumentSnapshot>() {
            @Override
            public void onSuccess(DocumentSnapshot documentSnapshot) {
                if (documentSnapshot.exists()){
                    acontent[9] = documentSnapshot.getString(TIDBIT_KEY);
                    websource[9] = documentSnapshot.getString(SOURCE_KEY);
                    titles[9] = documentSnapshot.getString(HEADLINE_KEY);
                    Log.d(TAG, "Document was successfully retrieved: "+websource[3]+": "+acontent[3]);

                    //Re-populate the view with database content
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


    }//end onCreate



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
