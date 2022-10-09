export async function beforeStart(options, extensions) {
	window.getFromStorage = (key) => {
		localStorage.getItem(key);
	}

	window.setInStorage = (key, value) => {
		localStorage.setItem(key, value);
	}

	window.removeFromStorage = (key) => {
		localStorage.removeItem(key);
	}
}

export async function afterStarted() {
}