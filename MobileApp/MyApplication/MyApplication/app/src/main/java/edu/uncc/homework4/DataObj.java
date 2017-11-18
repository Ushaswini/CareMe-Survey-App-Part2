package edu.uncc.homework4;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by sunand on 11/9/17.
 */

public class DataObj implements Serializable {

    ArrayList<SurveyAnswer> data;

    public DataObj(){
        data = new ArrayList<>();
    }


    public ArrayList<SurveyAnswer> getUserResponseText() {
        return data;
    }

    public void setUserResponseText(ArrayList<SurveyAnswer> userResponseText) {
        data = userResponseText;
    }
}
