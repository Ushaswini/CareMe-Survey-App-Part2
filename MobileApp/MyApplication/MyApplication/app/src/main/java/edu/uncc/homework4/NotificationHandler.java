package edu.uncc.homework4;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;

/**
 * Created by Nitin on 11/14/2017.
 */

public class NotificationHandler extends BroadcastReceiver {

    @Override
    public void onReceive(Context context, Intent intent) {

        String packageName = context.getPackageName();
        Intent resultIntent = new Intent(context.getPackageManager().getLaunchIntentForPackage(packageName));
        resultIntent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK| Intent.FLAG_ACTIVITY_CLEAR_TASK);

    }
}
