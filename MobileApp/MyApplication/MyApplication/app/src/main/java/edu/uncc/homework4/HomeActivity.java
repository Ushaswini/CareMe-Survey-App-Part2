package edu.uncc.homework4;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.RecyclerView;

import com.pushbots.push.Pushbots;

import java.util.ArrayList;

public class HomeActivity extends NavigationDrawerActivity {



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);

        //Pushbots.sharedInstance().registerForRemoteNotifications();

    }
}
