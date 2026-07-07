import { describe, it, expect, vi } from 'vitest'
import { render, screen, fireEvent } from '@testing-library/react'
import { ChatInput } from './ChatInput'

describe('ChatInput', () => {
  it('renders input field and send button', () => {
    render(<ChatInput onSendMessage={vi.fn()} isTyping={false} />)

    const input = screen.getByPlaceholderText(/ask about setups/i)
    const button = screen.getByRole('button', { name: /send message/i })

    expect(input).toBeInTheDocument()
    expect(button).toBeInTheDocument()
    expect(button).toBeDisabled() // disabled by default when input is empty
  })

  it('updates value as user types and enables send button', () => {
    render(<ChatInput onSendMessage={vi.fn()} isTyping={false} />)

    const input = screen.getByPlaceholderText(/ask about setups/i) as HTMLInputElement
    const button = screen.getByRole('button', { name: /send message/i })

    fireEvent.change(input, { target: { value: 'Hello Meridian' } })
    expect(input.value).toBe('Hello Meridian')
    expect(button).not.toBeDisabled()
  })

  it('fires onSendMessage callback on submit and clears input', () => {
    const onSendMessage = vi.fn()
    render(<ChatInput onSendMessage={onSendMessage} isTyping={false} />)

    const input = screen.getByPlaceholderText(/ask about setups/i) as HTMLInputElement
    fireEvent.change(input, { target: { value: 'Hello' } })

    const form = screen.getByRole('button', { name: /send message/i }).closest('form')!
    fireEvent.submit(form)

    expect(onSendMessage).toHaveBeenCalledWith('Hello')
    expect(input.value).toBe('')
  })

  it('disables input and button when isTyping is true', () => {
    render(<ChatInput onSendMessage={vi.fn()} isTyping={true} />)

    const input = screen.getByPlaceholderText(/ask about setups/i) as HTMLInputElement
    const button = screen.getByRole('button', { name: /send message/i })

    expect(input).toBeDisabled()
    expect(button).toBeDisabled()
  })
})
