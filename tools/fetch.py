import requests

def fetch_contents(url):
    try:
        response = requests.get(url)
        response.raise_for_status()  # Raises an HTTPError for bad responses (4xx and 5xx)
        return response.text
    except requests.exceptions.RequestException as e:
        print(f"An error occurred: {e}")
        return None

# Example usage
url = 'https://raw.githubusercontent.com/CS3-USTP/CSQB-API/main/tunnels.txt'
content = fetch_contents(url)
if content is not None:
    print(content)
