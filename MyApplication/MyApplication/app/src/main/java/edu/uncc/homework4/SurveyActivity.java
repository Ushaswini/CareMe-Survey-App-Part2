package edu.uncc.homework4;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.annotation.TargetApi;
import android.content.Intent;
import android.os.Build;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.ButtonBarLayout;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.SurfaceView;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.google.gson.Gson;

import org.json.JSONException;
import org.json.JSONObject;
import org.researchstack.backbone.StorageAccess;
import org.researchstack.backbone.answerformat.AnswerFormat;
import org.researchstack.backbone.answerformat.ChoiceAnswerFormat;
import org.researchstack.backbone.answerformat.TextAnswerFormat;
import org.researchstack.backbone.model.Choice;
import org.researchstack.backbone.result.StepResult;
import org.researchstack.backbone.result.TaskResult;
import org.researchstack.backbone.step.InstructionStep;
import org.researchstack.backbone.step.QuestionStep;
import org.researchstack.backbone.step.Step;
import org.researchstack.backbone.task.OrderedTask;
import org.researchstack.backbone.ui.ViewTaskActivity;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.FormBody;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

public class SurveyActivity extends AppCompatActivity {

    private static final int REQUEST_SURVEY_1  = 1;
    private static final int REQUEST_SURVEY_2  = 2;
    private static final int REQUEST_SURVEY_3  = 3;
    String access_token;
    SurveyResponse surveyResponse;
    private ProgressBar mProgressView;
    Button btnTakeSurvey;


    Boolean hasHighBloodPresure = false;
    Boolean skipToDietSection = false;

    private final OkHttpClient client = new OkHttpClient();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_survey);

        if(getIntent().getExtras() != null){
            access_token = getIntent().getExtras().getString("access_token");
        }


        mProgressView = new ProgressBar(SurveyActivity.this);

        btnTakeSurvey = (Button) findViewById(R.id.btnTakeSurvey);
        btnTakeSurvey.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                surveyResponse = new SurveyResponse();
                surveyResponse.setSurveyId("3045b14f-74bc-4c93-99b7-c430660c6189");
                surveyResponse.setUserId("57aeef8f-3140-4295-87eb-8f304cd4cfe6");
                surveyResponse.setStudyGroupId("1");

                displayInfo1Survey();
            }
        });

    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.main_menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle item selection
        switch (item.getItemId()) {
            case R.id.btnLogout:
                LogOut();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    private void LogOut(){
        Intent intent = new Intent(this, LoginActivity.class);
        startActivity(intent);
    }

    private void displaySurvey() {

        List<Step> steps = new ArrayList<>();

        //TODO : Instructions for survey
        InstructionStep instructionStep = new InstructionStep("survey_instruction_step",
                "Welcome!",
                "We need to collect just a little health information from you before we begin. Circle the correct answer");
        steps.add(instructionStep);

        //TODO : Text input question
        TextAnswerFormat format = new TextAnswerFormat(20);

        QuestionStep nameStep = new QuestionStep("name", "What is your name?", format);
        nameStep.setPlaceholder("Name");
        nameStep.setOptional(false);
        steps.add(nameStep);

        //TODO : Text choice question
        AnswerFormat questionFormat = new ChoiceAnswerFormat(AnswerFormat.ChoiceAnswerStyle
                .SingleChoice,
                new Choice<>("Create a ResearchKit App", 0),
                new Choice<>("Seek the Holy Grail", 1),
                new Choice<>("Find a shrubbery", 2));

        QuestionStep questionStep = new QuestionStep("quest_step", "What is your quest?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);


        //TODO : Text choice question multi choice
        AnswerFormat questionFormat1 = new ChoiceAnswerFormat(AnswerFormat.ChoiceAnswerStyle
                .MultipleChoice,
                new Choice<>("Create a ResearchKit App", 0),
                new Choice<>("Seek the Holy Grail", 1),
                new Choice<>("Find a shrubbery", 2));

        QuestionStep questionStep1 = new QuestionStep("quest_step1", "What is your quest?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep1);

        AnswerFormat colorAnswerFormat = new ImageChoiceAnswerFormat(AnswerFormat.ChoiceAnswerStyle
                .SingleChoice,
                new Choice<>("Red", R.mipmap.ic_launcher_round),
                new Choice<>("Orange", R.mipmap.ic_launcher_round),
                new Choice<>("Yellow", R.mipmap.ic_launcher_round),
                new Choice<>("Green", R.mipmap.ic_launcher_round),
                new Choice<>("Blue", R.mipmap.ic_launcher_round),
                new Choice<>("Purple", R.mipmap.ic_launcher_round));

        QuestionStep colorStep = new QuestionStep("color_step", "What is your favorite color?",
                colorAnswerFormat);
        colorStep.setOptional(false);
        steps.add(colorStep);


        InstructionStep summaryStep = new InstructionStep("survey_summary_step",
                "Right. Off you go!",
                "That was easy!");
        steps.add(summaryStep);

        OrderedTask task = new OrderedTask("survey_task", steps);

        Intent intent = ViewTaskActivity.newIntent(this, task);
        startActivityForResult(intent, REQUEST_SURVEY_1);

    }

    private void displayInfo1Survey(){

        Intent intent = ViewTaskActivity.newIntent(SurveyActivity.this, CreateSurveys.createInfo1Survey());
        startActivityForResult(intent, REQUEST_SURVEY_1);

    }

    private void displayInfo2Survey(){
        Intent intent = ViewTaskActivity.newIntent(SurveyActivity.this, CreateSurveys.createInfo2Survey());
        startActivityForResult(intent, REQUEST_SURVEY_2);


    }

    private void displayMainSurvey(){

        Intent intent = ViewTaskActivity.newIntent(SurveyActivity.this, CreateSurveys.createMainSurvey(skipToDietSection));
        startActivityForResult(intent, REQUEST_SURVEY_3);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if(requestCode ==  REQUEST_SURVEY_1 && resultCode == RESULT_OK){
            processSurvey1Result((TaskResult) data.getSerializableExtra(ViewTaskActivity.EXTRA_TASK_RESULT));
        }
        else if(requestCode ==  REQUEST_SURVEY_2 && resultCode == RESULT_OK){
            processSurvey2Result((TaskResult) data.getSerializableExtra(ViewTaskActivity.EXTRA_TASK_RESULT));
        }
        else if(requestCode ==  REQUEST_SURVEY_3 && resultCode == RESULT_OK){
            processSurvey3Result((TaskResult) data.getSerializableExtra(ViewTaskActivity.EXTRA_TASK_RESULT));
        }
    }

    private void processSurvey1Result(TaskResult result)
    {

        hasHighBloodPresure = result.getStepResult("quest_info_1").getResult().toString().equals("0") ;

        if(hasHighBloodPresure)
            displayInfo2Survey();
        else {
            skipToDietSection = true;
            displayMainSurvey();
        }

    }

    private void processSurvey2Result(TaskResult result)
    {

        skipToDietSection = result.getStepResult("quest_info_2").getResult().toString().equals("1") ;

        displayMainSurvey();

    }

    private void processSurvey3Result(TaskResult result)
    {
        ArrayList<SurveyAnswer> surveyAnswerArrayList = new ArrayList<>();
        SurveyAnswer surveyAnswer;

        for(String id : result.getResults().keySet())
        {
            StepResult stepResult = result.getStepResult(id);
            if(stepResult!=null) {
                surveyAnswer = new SurveyAnswer();
                surveyAnswer.setQuestionNumber(Integer.parseInt(id));
                surveyAnswer.setAnswerValue(Integer.parseInt(stepResult.getResult().toString()));
                surveyAnswer.setAnswerText(stepResult.getResult().toString());

                surveyAnswerArrayList.add(surveyAnswer);

            }
        }

        surveyResponse.setUserResponseText(surveyAnswerArrayList);
        surveyResponse.setSurveyResponseReceivedTime(new Date());

        try {
            PostSurveyResponse(surveyResponse);
        } catch (Exception e) {
            e.printStackTrace();
        }


    }

    @TargetApi(Build.VERSION_CODES.HONEYCOMB_MR2)
    private void showProgress(final boolean show) {
        // On Honeycomb MR2 we have the ViewPropertyAnimator APIs, which allow
        // for very easy animations. If available, use these APIs to fade-in
        // the progress spinner.
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB_MR2) {
            int shortAnimTime = getResources().getInteger(android.R.integer.config_shortAnimTime);

            mProgressView.setVisibility(show ? View.VISIBLE : View.GONE);
            mProgressView.animate().setDuration(shortAnimTime).alpha(
                    show ? 1 : 0).setListener(new AnimatorListenerAdapter() {
                @Override
                public void onAnimationEnd(Animator animation) {
                    mProgressView.setVisibility(show ? View.VISIBLE : View.GONE);
                }
            });
        } else {
            // The ViewPropertyAnimator APIs are not available, so simply show
            // and hide the relevant UI components.
            mProgressView.setVisibility(show ? View.VISIBLE : View.GONE);
        }
    }

    public void PostSurveyResponse(SurveyResponse surveyResponse) throws Exception {

        Gson gson = new Gson();
        DataObj data = new DataObj();
        data.setUserResponseText(surveyResponse.getUserResponseText());
        String surveyResponse_json = gson.toJson(data);

        RequestBody formBody = new FormBody.Builder()
                .add("SurveyId", surveyResponse.getSurveyId())
                .add("UserId", surveyResponse.getUserId())
                .add("StudyGroupId", surveyResponse.getStudyGroupId())
                .add("SurveyResponseReceivedTime", surveyResponse.getSurveyResponseReceivedTime())
                .add("UserResponseText", surveyResponse_json)
                .build();


        Log.d("demo", surveyResponse_json);

        final Request request = new Request.Builder()
                .url("http://caremesurvey.azurewebsites.net/api/SurveyResponses")
                .header("Content-Type","application/x-www-form-urlencoded")
                .header("Authorization","Bearer "+ access_token)
                .post(formBody)
                .build();

        client.newCall(request).enqueue(new Callback() {
            @Override
            public void onFailure(Call call, IOException e) {
                runOnUiThread(new Runnable() {
                    public void run() {
                        showProgress(false);

                        Log.d("demo", "request failed");

                    }
                });

            }

            @Override
            public void onResponse(Call call, Response response) throws IOException {

                String response1 = response.body().toString();
                Log.d("demo", response.isSuccessful() +"");

                Log.d("demo", response1);
                runOnUiThread(new Runnable() {
                    public void run() {
                        showProgress(false);
                        Toast.makeText(SurveyActivity.this, "Survey Finished", Toast.LENGTH_LONG).show();

                    }
                });

            }
        });
    }

}


