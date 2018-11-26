package com.example.vlad.tidbit;

import android.content.Intent;
import android.graphics.drawable.ColorDrawable;
import android.os.Build;
import android.support.v4.app.NavUtils;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.view.MenuItem;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;

public class ActivitySettings extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);

        //Toolbar stuff
        final Toolbar mToolbar = findViewById(R.id.toolbar_activity);
        mToolbar.setTitle("Settings");
        mToolbar.setTitleTextColor(0xFFFFFFFF);
        mToolbar.setNavigationIcon(R.drawable.ic_arrow_left_white);
        mToolbar.setNavigationOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                finish();
            }
        });

        //Button stuff
        Button redButton = (Button) findViewById(R.id.button_red);
        Button greenButton = (Button) findViewById(R.id.button_green);
        Button purpleButton = (Button) findViewById(R.id.button_purple);
        redButton.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                mToolbar.setBackgroundColor(getResources().getColor(R.color.colorPrimaryRed));
                Window window = getWindow();
                window.addFlags(WindowManager.LayoutParams.FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS);
                window.setStatusBarColor(getResources().getColor(R.color.colorPrimaryDarkRed));
            }
        });
        greenButton.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                mToolbar.setBackgroundColor(getResources().getColor(R.color.colorPrimaryGreen));
                Window window = getWindow();
                window.addFlags(WindowManager.LayoutParams.FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS);
                window.setStatusBarColor(getResources().getColor(R.color.colorPrimaryDarkGreen));
            }
        });
        purpleButton.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                mToolbar.setBackgroundColor(getResources().getColor(R.color.colorPrimary));
                Window window = getWindow();
                window.addFlags(WindowManager.LayoutParams.FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS);
                window.setStatusBarColor(getResources().getColor(R.color.colorPrimaryDark));
            }
        });
    }
}
