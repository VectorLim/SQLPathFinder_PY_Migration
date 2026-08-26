from ollama import Client

client = Client(host="http://localhost:11434")

response = client.chat(
    model="qwen3.6:35b-a3b-q4_K_M",
    messages=[
        {
            "role": "system",
            "content": "You are a concise Python coding assistant.",
        },
        {
            "role": "user",
            "content": "Write a Python function that adds two numbers.",
        },
    ],
    options={
        "temperature": 0.3,
        "num_ctx": 32768,
    },
)

print(response.message.content)