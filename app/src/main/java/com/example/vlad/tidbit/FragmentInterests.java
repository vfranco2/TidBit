package com.example.vlad.tidbit;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.RadioGroup;
import android.widget.TextView;


public class FragmentInterests extends Fragment {



    public FragmentInterests(){

    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        // Inflate the layout for this fragment.
        final View rootView = inflater.inflate(R.layout.fragment_fragment_interests,
                container, false);



        return rootView;
    }

    public static FragmentInterests newInstance() {
        return new FragmentInterests();
    }
}
