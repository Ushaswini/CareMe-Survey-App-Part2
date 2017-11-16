package edu.uncc.homework4;

import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import java.util.List;

/**
 * Created by Nitin on 11/14/2017.
 */

public class MessagesRecyclerAdapter extends RecyclerView.Adapter<MessagesRecyclerAdapter.ViewHolder>{


    private OnItemClickListener listener;

    public interface OnItemClickListener {
        void onItemClick(View itemView, int position);
    }
    public void setOnItemClickListener(OnItemClickListener listener) {
        this.listener = listener;
    }

    public class ViewHolder extends RecyclerView.ViewHolder {

        public TextView message;

        Context vContext;

        public ViewHolder(Context context,final View itemView) {
            super(itemView);

            message = (TextView) itemView.findViewById(R.id.textViewMessage);

            vContext = context;

            itemView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    if (listener != null) {
                        int position = getAdapterPosition();
                        if (position != RecyclerView.NO_POSITION) {
                            listener.onItemClick(itemView, position);
                        }
                    }
                }
            });
        }

    }

    List<String> messages;
    Context mContext;

    public MessagesRecyclerAdapter(List<String> messages, Context mContext) {
        this.messages = messages;
        this.mContext = mContext;
    }

    private Context getContext() {
        return mContext;
    }

    @Override
    public MessagesRecyclerAdapter.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {

        Context context = parent.getContext();
        LayoutInflater inflater = LayoutInflater.from(context);
        View contactView = inflater.inflate(R.layout.messages_layout, parent, false);
        ViewHolder viewHolder = new ViewHolder(getContext(),contactView);
        return viewHolder;
    }

    @Override
    public void onBindViewHolder(MessagesRecyclerAdapter.ViewHolder holder, int position) {
        //MusicTrack track = tracks.get(position);
        String message = messages.get(position);
        holder.message.setText(message);

    }

    @Override
    public int getItemCount() {
        return messages.size();
    }


}

