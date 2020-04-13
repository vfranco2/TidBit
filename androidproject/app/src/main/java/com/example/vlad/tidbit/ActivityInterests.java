package com.example.vlad.tidbit;

import android.content.Intent;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;
import androidx.appcompat.app.AppCompatActivity;
import android.os.Bundle;
import android.util.DisplayMetrics;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;

//----------------------------------------------------
//TidBit created and developed by Vlad Franco, Harrison Lavins, and Yousif Kashat
//This version is considered the "Legacy" version as of April 5, 2020.
//----------------------------------------------------

public class ActivityInterests extends AppCompatActivity implements View.OnClickListener{

    private CheckBox cbWod, cbPod, cbSports, cbAuto, cbFashion, cbFilm, cbFinance, cbFood, cbMusic, cbTech;
    private Button btnSave;
    boolean interested[] = new boolean[10];

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_interests);

        //Popup stuff
        DisplayMetrics inDisplay = new DisplayMetrics();
        getWindowManager().getDefaultDisplay().getMetrics(inDisplay);
        int width = inDisplay.widthPixels;
        int height = inDisplay.heightPixels;
        getWindow().setLayout((int)(width*.8),(int)(height*.8));

        //Preference stuff
        initControls();
        loadSavedPreferences();
    }

    private void loadSavedPreferences(){
        SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
        String[] boolKeys = {"wodvalue", "podvalue", "sportsvalue", "autovalue", "fashionvalue", "filmvalue", "financevalue", "foodvalue", "musicvalue", "techvalue"};
        for (int h = 0; h<10; h++){
            interested[h] = preferences.getBoolean(boolKeys[h], false);
        }
        cbWod.setChecked(interested[0]);
        cbPod.setChecked(interested[1]);
        cbSports.setChecked(interested[2]);
        cbAuto.setChecked(interested[3]);
        cbFashion.setChecked(interested[4]);
        cbFilm.setChecked(interested[5]);
        cbFinance.setChecked(interested[6]);
        cbFood.setChecked(interested[7]);
        cbMusic.setChecked(interested[8]);
        cbTech.setChecked(interested[9]);
    }

    private void savePreferences(String key, boolean value) {
        SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
        SharedPreferences.Editor editor = preferences.edit();
        editor.putBoolean(key, value);
        editor.commit();
    }

    private void initControls(){
        cbWod = findViewById(R.id.checkbox_word);
        cbPod = findViewById(R.id.checkbox_photo);
        cbSports = findViewById(R.id.checkbox_sports);
        cbAuto = findViewById(R.id.checkbox_auto);
        cbFashion = findViewById(R.id.checkbox_fashion);
        cbFilm = findViewById(R.id.checkbox_movie);
        cbFinance = findViewById(R.id.checkbox_finance);
        cbFood = findViewById(R.id.checkbox_food);
        cbMusic = findViewById(R.id.checkbox_music);
        cbTech = findViewById(R.id.checkbox_tech);
        btnSave = findViewById(R.id.btnSave);
        btnSave.setOnClickListener(this);
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.btnSave:
                savePreferences("wodvalue", cbWod.isChecked());
                savePreferences("podvalue", cbPod.isChecked());
                savePreferences("sportsvalue", cbSports.isChecked());
                savePreferences("autovalue", cbAuto.isChecked());
                savePreferences("fashionvalue", cbFashion.isChecked());
                savePreferences("filmvalue", cbFilm.isChecked());
                savePreferences("financevalue", cbFinance.isChecked());
                savePreferences("foodvalue", cbFood.isChecked());
                savePreferences("musicvalue", cbMusic.isChecked());
                savePreferences("techvalue", cbTech.isChecked());
                Intent boolintent = new Intent(getApplicationContext(), MainActivity.class);
                startActivity(boolintent);
                break;
        }
    }
}
