package com.example.vlad.tidbit;

import android.content.Intent;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;
import androidx.annotation.NonNull;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.browser.customtabs.CustomTabsIntent;
import androidx.core.content.ContextCompat;
import androidx.core.view.GravityCompat;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import androidx.swiperefreshlayout.widget.SwipeRefreshLayout;

import android.os.Bundle;
import android.util.Log;
import android.net.*;
import android.view.View;
import android.widget.Toast;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.google.firebase.firestore.*;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ThreadLocalRandom;

//----------------------------------------------------
//TidBit created and developed by Vlad Franco, Harrison Lavins, and Yousif Kashat
//----------------------------------------------------

public class MainActivity extends AppCompatActivity {

    // Used to load the 'native-lib' library on application startup.
    static {
        System.loadLibrary("native-lib");
    }

    //private DrawerLayout mDrawerLayout;
    private RecyclerView mRecyclerView;
    private RecyclerView.LayoutManager mLayoutManager;
    private List<ArticleHolder> articleList;
    private ArticleAdapter articleAdapter;
    private SwipeRefreshLayout mRefresh;

    int max = 4;
    int randomNum = ThreadLocalRandom.current().nextInt(0, max +1);

    //Firebase references
    String[] cardV = {"gaming/card_","photography/card_","cars/card_","fashion/card_","movie/card_",
            "finance/card_","cuisine/card_","music/card_","sports/card_","technology/card_"};
    DocumentReference gameRef = FirebaseFirestore.getInstance().document(cardV[0]+Integer.toString(randomNum));
    DocumentReference photosRef = FirebaseFirestore.getInstance().document(cardV[1]+Integer.toString(randomNum));
    DocumentReference carsRef = FirebaseFirestore.getInstance().document(cardV[2]+Integer.toString(randomNum));
    DocumentReference fashionRef = FirebaseFirestore.getInstance().document(cardV[3]+Integer.toString(randomNum));
    DocumentReference movieRef = FirebaseFirestore.getInstance().document(cardV[4]+Integer.toString(randomNum));
    DocumentReference financeRef = FirebaseFirestore.getInstance().document(cardV[5]+Integer.toString(randomNum));
    DocumentReference foodRef = FirebaseFirestore.getInstance().document(cardV[6]+Integer.toString(randomNum));
    DocumentReference musicRef = FirebaseFirestore.getInstance().document(cardV[7]+Integer.toString(randomNum));
    DocumentReference sportsRef = FirebaseFirestore.getInstance().document(cardV[8]+Integer.toString(randomNum));
    DocumentReference techRef = FirebaseFirestore.getInstance().document(cardV[9]+Integer.toString(randomNum));

    private DocumentReference[] firebaseDocs = {gameRef, photosRef, sportsRef, carsRef, fashionRef,
            movieRef, financeRef, foodRef, musicRef, techRef
    };

    //Used for getting data fields in Firebase
    public static final String TAG = "TIDBIT";
    public static final String TIDBIT_KEY = "tidbit";
    public static final String SOURCE_KEY = "source";
    public static final String HEADLINE_KEY = "headline";
    public static final String IMAGE_KEY = "image_URL";
    public static final String URL_KEY = "article_link";
    int counter = 0;

    int[] category = {
            R.drawable.ic_action_joypad, R.drawable.ic_action_camera,
            R.drawable.ic_action_ball, R.drawable.ic_action_car,
            R.drawable.ic_action_glasses, R.drawable.ic_action_movie,
            R.drawable.ic_action_line_chart, R.drawable.ic_action_restaurant,
            R.drawable.ic_action_record, R.drawable.ic_action_laptop};

    String[] titles = {"Content not found!", "Content not found!",
            "Content not found!", "Content not found!",
            "Content not found!", "Content not found!",
            "Content not found!", "Content not found!",
            "Content not found!", "Content not found!"};

    String[] websource = {"", "", "", "", "", "", "", "", "", "",};

    static String[] acontent = {"Content not found!", "Content not found!",
            "Content not found!", "Content not found!",
            "Content not found!", "Content not found!",
            "Content not found!", "Content not found!",
            "Content not found!", "Content not found!"};

    String[] source_URLs = {"", "", "", "", "", "", "", "", "", "",};

    String[] image_URLs = {
            "https://imgplaceholder.com/420x320/ff7f7f/333333/fa-image",
            "https://imgplaceholder.com/420x320/ff7f7f/333333/fa-image",
            "https://imgplaceholder.com/420x320/ff7f7f/333333/fa-image",
            "https://imgplaceholder.com/420x320/ff7f7f/333333/fa-image",
            "https://imgplaceholder.com/420x320/ff7f7f/333333/fa-image",
            "https://imgplaceholder.com/420x320/ff7f7f/333333/fa-image",
            "https://imgplaceholder.com/420x320/ff7f7f/333333/fa-image",
            "https://imgplaceholder.com/420x320/ff7f7f/333333/fa-image",
            "https://imgplaceholder.com/420x320/ff7f7f/333333/fa-image",
            "https://imgplaceholder.com/420x320/ff7f7f/333333/fa-image",
            "https://imgplaceholder.com/420x320/ff7f7f/333333/fa-image",};

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mRecyclerView = findViewById(R.id.recycler_view);
        mLayoutManager = new LinearLayoutManager(this);
        mRefresh = (SwipeRefreshLayout) findViewById(R.id.swiperefresh);

        FloatingActionButton fab = findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                startActivity(new Intent (MainActivity.this, ActivityInterests.class));
            }
        });

        //Toolbar stuff
        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        toolbar.setTitleTextColor(android.graphics.Color.WHITE);

        //Recycler view stuff, fills with default images/text
        mRecyclerView.setLayoutManager(mLayoutManager);
        articleList = new ArrayList<>();

        for (int i = 0; i < titles.length; i++){
            ArticleHolder article = new ArticleHolder(category[i], titles[i], websource[i], acontent[i], source_URLs[i], image_URLs[i]);
            articleList.add(article);
        }

        articleAdapter = new ArticleAdapter(articleList, getApplicationContext());
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
                        image_URLs[j] = documentSnapshot.getString(IMAGE_KEY);
                        Log.d(TAG, "Document was successfully retrieved: "+websource[3]+": "+acontent[3]);

                        //Re-populate the view with database content at that location
                        ArticleHolder article = new ArticleHolder(category[j], titles[j], websource[j], acontent[j], source_URLs[j], image_URLs[j]);
                        articleList.set(j, article);

                        counter++;
                        System.out.println(counter);
                        if (counter == 10) {
                            System.out.println("new code");
                            articleAdapter.printList();
                            System.out.println(articleList.get(5));
                            coolList(articleList);
                        }
                        //Click handling for each article
                        articleAdapter.setOnItemClickListener(new ArticleAdapter.OnItemClickListener() {
                            @Override
                            public void onItemClick(int position) {
                                Uri webpage = Uri.parse(source_URLs[position]);
                                Toast.makeText(getApplicationContext(), "You clicked " + position, Toast.LENGTH_SHORT).show();
                                //Call launchWeb method, holds web page intent
                                launchWeb(webpage);
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

        mRefresh.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                //articleAdapter.notifyDataSetChanged();
                Intent refIntent = new Intent(getApplicationContext(), MainActivity.class);
                startActivity(refIntent);
                mRefresh.setRefreshing(false);
            }
        });
    }

    public void launchWeb(Uri holder){
        CustomTabsIntent.Builder builder = new CustomTabsIntent.Builder();
        final CustomTabsIntent intent = new CustomTabsIntent.Builder().build();

        intent.launchUrl(this, holder);
        builder.setToolbarColor(ContextCompat.getColor(this, R.color.colorPrimary));
    }

    public void coolList(List<ArticleHolder> articleList){
        List<ArticleHolder> newList;
        newList = new ArrayList<>();
        SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);

        boolean[] interested = new boolean[10];
        String[] boolKeys = {"wodvalue", "podvalue", "sportsvalue", "autovalue", "fashionvalue", "filmvalue", "financevalue", "foodvalue", "musicvalue", "techvalue"};
        for (int h = 0; h<10; h++){
            interested[h] = preferences.getBoolean(boolKeys[h], false);
            if (interested[h]){
                newList.add(articleList.get(h));
            }
        }

        for (int i = 0; i < newList.size(); i++) {
            source_URLs[i] = newList.get(i).getArticleImage();
        }

        articleAdapter = new ArticleAdapter(newList,getApplicationContext());
        mRecyclerView.setAdapter(articleAdapter);
        articleAdapter.notifyDataSetChanged();
    }
}