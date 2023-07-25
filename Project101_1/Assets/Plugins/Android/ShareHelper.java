// Save this code as ShareHelper.java in your Android project's plugins folder (Assets/Plugins/Android).
package com.yourcompany.androidplugin;

import android.content.Intent;
import android.net.Uri;
import android.os.Environment;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import java.io.File;
import java.util.List;

public class ShareHelper {
    public static void ShareScreenshot(String imagePath, String message) {
        File imageFile = new File(imagePath);
        Uri imageUri = Uri.fromFile(imageFile);

        Intent shareIntent = new Intent(Intent.ACTION_SEND);
        shareIntent.setType("image/png");
        shareIntent.putExtra(Intent.EXTRA_STREAM, imageUri);
        shareIntent.putExtra(Intent.EXTRA_TEXT, message);

        // Check if Instagram is installed on the device
        if (isInstagramInstalled(UnityPlayer.currentActivity)) {
            shareIntent.setPackage("com.instagram.android");
        }

        Intent chooserIntent = Intent.createChooser(shareIntent, "Share Screenshot");
        chooserIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        UnityPlayer.currentActivity.startActivity(chooserIntent);
    }

    private static boolean isInstagramInstalled(Context context) {
        try {
            ApplicationInfo info = context.getPackageManager().getApplicationInfo("com.instagram.android", 0);
            return info != null;
        } catch (NameNotFoundException e) {
            return false;
        }
    }
}
