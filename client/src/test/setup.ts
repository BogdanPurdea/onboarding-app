import '@testing-library/jest-dom';

// Simple mock for localStorage to ensure compatibility in all Node/JSDOM environments
class LocalStorageMock {
  private store: Record<string, string> = {};

  clear() {
    this.store = {};
  }

  getItem(key: string) {
    return this.store[key] || null;
  }

  setItem(key: string, value: string) {
    this.store[key] = String(value);
  }

  removeItem(key: string) {
    delete this.store[key];
  }

  get length() {
    return Object.keys(this.store).length;
  }

  key(index: number) {
    const keys = Object.keys(this.store);
    return keys[index] || null;
  }
}

const mockLocalStorage = new LocalStorageMock();

Object.defineProperty(global, 'localStorage', {
  value: mockLocalStorage,
  writable: true
});

if (typeof window !== 'undefined') {
  Object.defineProperty(window, 'localStorage', {
    value: mockLocalStorage,
    writable: true
  });
}
